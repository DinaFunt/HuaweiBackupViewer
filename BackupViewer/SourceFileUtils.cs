using System.Collections.Generic;
using System.Collections.Specialized;
using System.IO;
using System.Linq;

namespace BackupViewer
{
    public static class SourceFileUtils
    {
        public static void HandleAllFiles(IList<string> files, string pathIn, string pathOut, HybridDictionary decryptMaterialDict, Decryptor decryptor)
        {
            var Handlers = new IFileHandler[]
            {
                new TarFileHandler(files.Where(entry => Path.GetExtension(entry).ToLower().Equals(".tar")).ToList()),
                new ApkFileHandler(files.Where(entry => Path.GetExtension(entry).ToLower().Equals(".apk")).ToList()),
                new DBFileHandler(files.Where(entry => Path.GetExtension(entry).ToLower().Equals(".db")).ToList()),
                new EncFileHandler(files.Where(entry => Path.GetExtension(entry).ToLower().Equals(".enc")).ToList()),
                new UnkFileHandler(files.Where(entry => Path.GetExtension(entry).ToLower().Equals(".unc")).ToList()),
            };
            foreach (var handler in Handlers)
            {
                handler.Handle(pathIn, pathOut, decryptMaterialDict, decryptor);
            }
        }
    }
}