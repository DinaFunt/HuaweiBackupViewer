using System.Collections.Generic;
using System.Collections.Specialized;
using System.IO;

namespace BackupViewer
{
    public class ApkFileHandler : IFileHandler
    {
        private IList<string> apkFiles;
        public ApkFileHandler(IList<string> apks)
        {
            apkFiles = apks;
        }

        public void Handle(string pathIn, string pathOut, HybridDictionary decryptMaterialDict, Decryptor decryptor)
        {
            if (apkFiles.Count > 0)
            {
                string dataApkDir = Path.Combine(pathOut, @"data\app");
                Directory.CreateDirectory(dataApkDir);

                foreach (string entry in apkFiles)
                {
                    string filename = Path.GetFileName(entry);
                    string destFile = Path.Combine(dataApkDir, filename + "-1");
                    Directory.CreateDirectory(destFile);
                    destFile = Path.Combine(destFile, "base.apk");
                    File.Copy(entry, destFile);
                }

            }
        }
    }
}