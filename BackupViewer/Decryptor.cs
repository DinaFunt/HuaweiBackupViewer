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

        private string _upwd { get; }
        private string _ePerBackupkey;
        private byte[] _ePerBackupKeyBytes;
        private string _pwkeySalt;
        private byte[] _pwkeySaltBytes;
        private string _checkMsg;
        private byte[] _checkMsgBytes;
        private string _bkey;
        private byte[] _bkeyBytes;
        private byte[] _bkeySha256;

        internal Decryptor(string password)
        {
            _upwd = password;
            IsValid = false;
            _ePerBackupkey = null;
            _pwkeySalt = null;
            TypeAttch = 0;
            _checkMsg = null;
            _bkey = null;
            _bkeySha256 = null;
        }


        internal bool IsValid { get; private set; }
        internal int TypeAttch { get; set; }

        internal string EPerBackupkey
        {
            get { return _ePerBackupkey; }
            set
            {
                if (!String.IsNullOrEmpty(value))
                {
                    _ePerBackupkey = value;
                    _ePerBackupKeyBytes = DecryptMaterial.Unhexlify(value);
                }
            }
        }

        internal string PwkeySalt
        {
            get { return _pwkeySalt; }
            set
            {
                if (!String.IsNullOrEmpty(value))
                {
                    _pwkeySalt = value;
                    _pwkeySaltBytes = DecryptMaterial.Unhexlify(value);
                }
            }
        }

        internal string CheckMsg
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

        internal string Bkey
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

        void DecryptBkeyV4()
        {
            throw new NotImplementedException();
        }

        internal void Initialize()
        {
            if (IsValid)
            {
                return;
            }

            if (TypeAttch != 3)
            {
                Console.WriteLine("crypto_init: type_attch *should be* 3!");
                return;
            }

            if (!String.IsNullOrEmpty(EPerBackupkey) && !String.IsNullOrEmpty(PwkeySalt))
            {
                Console.WriteLine("crypto_init: using version 4");
                DecryptBkeyV4();
            }
            else
            {
                Console.WriteLine("crypto_init: using version 3");
                _bkey = _upwd;
            }

            var passwordBytes = Encoding.UTF8.GetBytes(_bkey);
            passwordBytes = SHA256.Create().ComputeHash(passwordBytes);
            _bkeySha256 = passwordBytes.Take(16).ToArray();

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

        internal byte[] DecryptPackage(DecryptMaterial dec_material, byte[] data)
        {
            if (IsValid == false)
                Console.WriteLine("well, it is hard to decrypt with a wrong key.");

            if (String.IsNullOrEmpty(dec_material.EncMsgV3))
            {
                Console.WriteLine("cannot decrypt with an empty encMsgV3!");
                return null;
            }

            byte[] salt = dec_material.EncMsgV3Bytes.Take(32).ToArray();
            byte[] counter_iv = dec_material.EncMsgV3Bytes.Skip(32).ToArray();

            byte[] key = DecryptorUtils.PBKDF2_SHA256_GetBytes(Encoding.UTF8.GetBytes(_bkey), salt, Dklen, Count);

            MemoryStream output = new MemoryStream();
            MemoryStream input = new MemoryStream(data);
            DecryptorUtils.AES_CTR_Transform(key, counter_iv, input, output);

            return output.ToArray();
        }

        internal byte[] DecryptFile(DecryptMaterial decMaterial, byte[] data)
        {
            if (IsValid == false)
                Console.WriteLine("well, it is hard to decrypt with a wrong key.");

            if (String.IsNullOrEmpty(decMaterial.IV))
            {
                Console.WriteLine("cannot decrypt with an empty iv!");
                return null;
            }

            using (MemoryStream output = new MemoryStream())
            {
                using (MemoryStream input = new MemoryStream(data))
                {
                    DecryptorUtils.AES_CTR_Transform(_bkeySha256, decMaterial._ivBytes, input, output);

                    return output.ToArray();
                }
            }
        }
    }
}