using System.Collections.Generic;
using System.Collections.Specialized;
using System.IO;

namespace BackupViewer
{
    public class DBFileHandler : IFileHandler
    {
        private IList<string> dbFiles;
        public DBFileHandler(IList<string> dbs)
        {
            dbFiles = dbs;
        }
        public void Handle(string pathIn, string pathOut, HybridDictionary decryptMaterialDict, Decryptor decryptor)
        {
            if (dbFiles.Count > 0)
            {
                string dataAppDir = Path.Combine(pathOut, "db");
                Directory.CreateDirectory(dataAppDir);

                foreach (string entry in dbFiles)
                {
                    byte[] cleartext = null;
                    string directory = Path.GetDirectoryName(entry);
                    string filename = Path.GetFileNameWithoutExtension(entry);
                    string skey = Path.Combine(directory, filename);
                    if (decryptMaterialDict.Contains(skey))
                    {
                        DecryptMaterial decMaterial = (DecryptMaterial) decryptMaterialDict[skey];
                        cleartext = decryptor.decrypt_package(decMaterial, File.ReadAllBytes(entry));
                    }

                    if (cleartext != null)
                    {
                        string destFile = Path.Combine(dataAppDir, Path.GetFileName(entry));
                        File.WriteAllBytes(destFile, cleartext);
                    }
                }
            }
        }
    }
}