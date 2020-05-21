using System;
using System.Text;

namespace BackupViewer
{
    internal class DecryptMaterial
    {
        internal static string Hexlify(byte[] ba)
        {
            StringBuilder hex = new StringBuilder(ba.Length * 2);
            foreach (byte b in ba)
            {
                hex.AppendFormat("{0:x2}", b);
            }
            return hex.ToString();
        }

        internal static byte[] Unhexlify(string hexvalue)
        {
            if (hexvalue.Length % 2 != 0)
            {
                hexvalue = "0" + hexvalue;
            }
            int len = hexvalue.Length / 2;
            byte[] bytes = new byte[len];
            for (int i = 0; i < len; i++)
            {
                string byteString = hexvalue.Substring(2 * i, 2);
                bytes[i] = Convert.ToByte(byteString, 16);
            }

            return bytes;
        }

        internal DecryptMaterial(string typeName)
        {
            TypeName = typeName;
            Name = null;
        }

        internal string TypeName { get; set; }

        internal string Name { get; set; }

        private string _encMsgV3;
        internal byte[] EncMsgV3Bytes;

        internal string EncMsgV3
        {
            get => _encMsgV3;
            set
            {
                if (!String.IsNullOrEmpty(value))
                {
                    _encMsgV3 = value;
                    EncMsgV3Bytes = Unhexlify(value);
                }
            }
        }

        private string _iv;
        internal byte[] _ivBytes;

        internal string IV
        {
            get => _iv;
            set
            {
                if (!String.IsNullOrEmpty(value))
                {
                    _iv = value;
                    _ivBytes = Unhexlify(value);
                }
            }
        }

        string _path;

        internal string Path
        {
            get => _path;
            set
            {
                if (!String.IsNullOrEmpty(value))
                {
                    _path = value;
                }
            }
        }

        internal bool DoCheck()
        {
            return Name != null && (EncMsgV3 != null || IV != null);
        }
    }
}