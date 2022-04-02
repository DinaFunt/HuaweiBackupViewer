using System.Collections.Specialized;

namespace BackupViewer
{
    public interface IFileHandler
    {
        void Handle(string pathIn, string pathOut, HybridDictionary decryptMaterialDict, Decryptor decryptor);
    }
}