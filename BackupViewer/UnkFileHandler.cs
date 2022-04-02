using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.IO;
using System.Linq;

namespace BackupViewer
{
    public class UnkFileHandler : IFileHandler
    {
        private IList<string> unkFiles;

        public UnkFileHandler(IList<string> unks)
        {
            unkFiles = unks;
        }
        public void Handle(string pathIn, string pathOut, HybridDictionary decryptMaterialDict, Decryptor decryptor)
        {
            if (unkFiles.Count <= 0) return;
            
            string dataUnkDir = Path.Combine(pathOut, "misc");
            Directory.CreateDirectory(dataUnkDir);

            foreach (string entry in unkFiles)
            {
                string commonPath = FindCommonPath(new List<string>()
                {
                    entry,
                    pathIn
                });
                string relativePath = entry.Replace(commonPath, "");
                relativePath = relativePath.TrimStart(new char[] {'\\', '/'});
                string destFile = Path.Combine(dataUnkDir, relativePath);
                Directory.CreateDirectory(Directory.GetParent(destFile).FullName);
                File.Copy(entry, destFile);
            }
        }
        
        private static string FindCommonPath(IEnumerable<string> paths)
        {
            const string separator = @"\";

            string commonPath = string.Empty;
            List<string> separatedPath = paths
                .First(str => str.Length == paths.Max(st2 => st2.Length))
                .Split(new string[] {separator}, StringSplitOptions.RemoveEmptyEntries)
                .ToList();

            foreach (string pathSegment in separatedPath.AsEnumerable())
            {
                if (commonPath.Length == 0 && paths.All(str => str.StartsWith(pathSegment)))
                {
                    commonPath = pathSegment;
                }
                else if (paths.All(str => str.StartsWith(commonPath + separator + pathSegment)))
                {
                    commonPath += separator + pathSegment;
                }
                else
                {
                    break;
                }
            }

            return commonPath;
        }
    }
}