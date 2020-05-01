using System.Collections.Generic;
using System.Collections.Specialized;
using System.IO;
using System.Linq;

namespace BackupViewer
{
    public static class BackupDecryptor
    {
        public static bool TryDecrypt(string pathIn, string pathOut, string userPassword, ref string message)
        {
            if (!Directory.Exists(pathIn))
            {
                message = "input backup folder does not exist!";
                return false;
            }

            if (Directory.Exists(pathOut))
            {
                List<string> files = Directory.GetFiles(pathOut, "*.*", SearchOption.AllDirectories).ToList();
                if (files.Count > 0)
                {
                    message = $"output folder contains {files.Count} files, cannot overwrite them!";
                    return false;
                }
            }
            else
            {
                Directory.CreateDirectory(pathOut);
            }

            IList<string> allFiles = Directory.GetFiles(pathIn, "*.*", SearchOption.AllDirectories).ToList();
            
            var decryptMaterialDict = new HybridDictionary();
            var decryptor = InitDecryptor(allFiles.Where(entry => Path.GetExtension(entry).ToLower().Equals(".xml")).ToList(), userPassword, ref decryptMaterialDict);

            decryptor.Initialize();
            if (decryptor.IsValid == false)
            {
                message = "decryption key is not good...";
                return false;
            }
            
            SourceFileUtils.HandleAllFiles(allFiles, pathIn, pathOut, decryptMaterialDict, decryptor);
            return true;
        }

        private static Decryptor InitDecryptor(IList<string> xmlFiles, string userPassword, ref HybridDictionary decryptMaterialDict)
        {
            Decryptor decryptor = new Decryptor(userPassword);

            foreach (string entry in xmlFiles)
            {
                string filename = Path.GetFileName(entry);
                if (filename.ToLower() == "info.xml")
                {
                    decryptMaterialDict = ParseXml.parse_info_xml(entry, ref decryptor, decryptMaterialDict);
                }
                else
                {
                    decryptMaterialDict = ParseXml.parse_xml(entry, decryptMaterialDict);
                }
            }

            return decryptor;
        }
    }
}