using System;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using System.Text;

namespace BackupViewer
{
     public class Decryptor
    {
        const int Count = 5000;
        const int Dklen = 32;

        private string Upwd { get; }
        private string _e_perbackupkey;
        private byte[] _e_perbackupkeyBytes;
        private string _pwkey_salt;
        private byte[] _pwkey_saltBytes;
        private string _checkMsg;
        private byte[] _checkMsgBytes;
        private string _bkey;
        private byte[] _bkeyBytes;
        private byte[] _bkey_sha256;

        internal Decryptor(string password)
        {
            Upwd = password;
            IsValid = false;
            _e_perbackupkey = null;
            _pwkey_salt = null;
            type_attch = 0;
            _checkMsg = null;
            _bkey = null;
            _bkey_sha256 = null;
        }


        internal bool IsValid { get; private set; }
        internal int type_attch { get; set; }

        internal string e_perbackupkey
        {
            get { return _e_perbackupkey; }
            set
            {
                if (!String.IsNullOrEmpty(value))
                {
                    _e_perbackupkey = value;
                    _e_perbackupkeyBytes = DecryptMaterial.Unhexlify(value);
                }
            }
        }

        internal string pwkey_salt
        {
            get { return _pwkey_salt; }
            set
            {
                if (!String.IsNullOrEmpty(value))
                {
                    _pwkey_salt = value;
                    _pwkey_saltBytes = DecryptMaterial.Unhexlify(value);
                }
            }
        }

        internal string checkMsg
        {
            get { return _checkMsg; }
            set
            {
                if (!String.IsNullOrEmpty(value))
                {
                    _checkMsg = value;
                    _checkMsgBytes = DecryptMaterial.Unhexlify(value);
                }
            }
        }

        internal string bkey
        {
            get { return _bkey; }
            private set
            {
                if (!String.IsNullOrEmpty(value))
                {
                    _bkey = value;
                    _bkeyBytes = DecryptMaterial.Unhexlify(value);
                }
            }
        }

        void __decrypt_bkey_v4()
        {
            throw new NotImplementedException();
        }

        internal void Initialize()
        {
            if (IsValid)
            {
                return;
            }

            if (type_attch != 3)
            {
                Console.WriteLine("crypto_init: type_attch *should be* 3!");
                return;
            }

            if (!String.IsNullOrEmpty(e_perbackupkey) && !String.IsNullOrEmpty(pwkey_salt))
            {
                Console.WriteLine("crypto_init: using version 4");
                __decrypt_bkey_v4();
            }
            else
            {
                Console.WriteLine("crypto_init: using version 3");
                _bkey = Upwd;
            }

            var passwordBytes = Encoding.UTF8.GetBytes(_bkey);
            passwordBytes = SHA256.Create().ComputeHash(passwordBytes);
            _bkey_sha256 = passwordBytes.Take(16).ToArray();

            byte[] salt = _checkMsgBytes.Skip(32).ToArray();
            byte[] res = DecryptorUtils.PBKDF2_SHA256_GetBytes(Encoding.UTF8.GetBytes(_bkey), salt, Dklen, Count);

            if (res.SequenceEqual(_checkMsgBytes.Take(32)))
            {
                Console.WriteLine("OK, backup key is correct!");
                IsValid = true;
            }
            else
            {
                Console.WriteLine("KO, backup key is wrong!");
                IsValid = false;
            }
        }

        internal byte[] decrypt_package(DecryptMaterial dec_material, byte[] data)
        {
            if (IsValid == false)
                Console.WriteLine("well, it is hard to decrypt with a wrong key.");

            if (String.IsNullOrEmpty(dec_material.EncMsgV3))
            {
                Console.WriteLine("cannot decrypt with an empty encMsgV3!");
                return null;
            }

            byte[] salt = dec_material._encMsgV3Bytes.Take(32).ToArray();
            byte[] counter_iv = dec_material._encMsgV3Bytes.Skip(32).ToArray();

            byte[] key = DecryptorUtils.PBKDF2_SHA256_GetBytes(Encoding.UTF8.GetBytes(_bkey), salt, Dklen, Count);

            MemoryStream output = new MemoryStream();
            MemoryStream input = new MemoryStream(data);
            DecryptorUtils.AES_CTR_Transform(key, counter_iv, input, output);

            return output.ToArray();
        }

        internal byte[] decrypt_file(DecryptMaterial dec_material, byte[] data)
        {
            if (IsValid == false)
                Console.WriteLine("well, it is hard to decrypt with a wrong key.");

            if (String.IsNullOrEmpty(dec_material.iv))
            {
                Console.WriteLine("cannot decrypt with an empty iv!");
                return null;
            }

            MemoryStream output = new MemoryStream();
            MemoryStream input = new MemoryStream(data);
            DecryptorUtils.AES_CTR_Transform(_bkey_sha256, dec_material._ivBytes, input, output);

            return output.ToArray();
        }
    }
}