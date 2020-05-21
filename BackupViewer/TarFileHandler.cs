using System.Collections.Generic;
using System.Collections.Specialized;
using System.IO;

namespace BackupViewer
{
    public class TarFileHandler : IFileHandler
    {
        private IList<string> tarFiles;

        public TarFileHandler(IList<string> tars)
        {
            tarFiles = tars;
        }
        public void Handle(string pathIn, string pathOut, HybridDictionary decryptMaterialDict, Decryptor decryptor)
        {
            if (tarFiles.Count <= 0) return;
            
            string dataAppDir = Path.Combine(pathOut, @"data\data");
            Directory.CreateDirectory(dataAppDir);

            foreach (string entry in tarFiles)
            {
                byte[] cleartext = null;
                string directory = Path.GetDirectoryName(entry);
                string filename = Path.GetFileNameWithoutExtension(entry);
                string skey = Path.Combine(directory, filename);
                if (decryptMaterialDict.Contains(skey))
                {
                    DecryptMaterial decMaterial = (DecryptMaterial) decryptMaterialDict[skey];
                    cleartext = decryptor.DecryptPackage(decMaterial, File.ReadAllBytes(entry));
                }

                if (cleartext != null)
                {
                    using (MemoryStream ms = new MemoryStream(cleartext))
                    {
                        TarUtils.UnTar(ms, dataAppDir);
                    }
                }
                else if (File.Exists(entry))
                {
                    using (StreamReader sr = new StreamReader(entry))
                    {
                        TarUtils.UnTar(sr.BaseStream, dataAppDir);
                    }
                }
            }
        }
    }
}