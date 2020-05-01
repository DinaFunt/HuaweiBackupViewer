using System.Collections.Generic;
using System.Collections.Specialized;
using System.IO;

namespace BackupViewer
{
    public class EncFileHandler : IFileHandler
    {
        private IList<string> encFiles;

        public EncFileHandler(IList<string> encs)
        {
            encFiles = encs;
        }

        public void Handle(string pathIn, string pathOut, HybridDictionary decryptMaterialDict, Decryptor decryptor)
        {
            if (encFiles.Count > 0)
            {
                for (int i = 1; i <= encFiles.Count; i++)
                {
                    string entry = encFiles[i - 1];
                    byte[] cleartext = null;
                    DecryptMaterial decMaterial = null;
                    string directory = Path.GetDirectoryName(entry);
                    string filename = Path.GetFileNameWithoutExtension(entry);
                    string skey = Path.Combine(directory, filename);
                    if (decryptMaterialDict.Contains(skey))
                    {
                        decMaterial = (DecryptMaterial) decryptMaterialDict[skey];
                        cleartext = decryptor.decrypt_file(decMaterial, File.ReadAllBytes(entry));
                    }

                    if (cleartext != null && decMaterial != null)
                    {
                        string destFile = pathOut;
                        string tmpPath = decMaterial.path.TrimStart(new char[] {'\\', '/'});
                        destFile = Path.Combine(destFile, tmpPath);
                        string destDir = Directory.GetParent(destFile).FullName;
                        Directory.CreateDirectory(destDir);
                        File.WriteAllBytes(destFile, cleartext);
                    }
                }
            }
        }
    }
}