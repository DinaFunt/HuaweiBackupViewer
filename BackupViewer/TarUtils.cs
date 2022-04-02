using System;
using System.IO;
using System.Text;

namespace BackupViewer
{
    public static class TarUtils
    {
        public static void UnTar(Stream inputStream, string outputDir)
        {
            var buffer = new byte[100];
            while (true)
            {
                inputStream.Read(buffer, 0, 100);
                var name = Encoding.ASCII.GetString(buffer).Trim('\0', ' ');
                
                if (String.IsNullOrWhiteSpace(name)) break;
                
                inputStream.Seek(24, SeekOrigin.Current);
                inputStream.Read(buffer, 0, 12);

                long size;
                string hex = Encoding.ASCII.GetString(buffer, 0, 12).Trim('\0', ' ');
                try
                {
                    size = Convert.ToInt64(hex, 8);
                }
                catch (Exception ex)
                {
                    throw new Exception("Could not parse hex: " + hex, ex);
                }

                inputStream.Seek(376L, SeekOrigin.Current);

                var output = Path.Combine(outputDir, name);
                if (size > 0) // ignores directory entries
                {
                    if (!Directory.Exists(Path.GetDirectoryName(output)))
                    {
                        Directory.CreateDirectory(Path.GetDirectoryName(output));
                    }
                    using (var str = File.Open(output, FileMode.OpenOrCreate, FileAccess.Write))
                    {
                        var buf = new byte[size];
                        inputStream.Read(buf, 0, buf.Length);
                        str.Write(buf, 0, buf.Length);
                    }
                }

                var pos = inputStream.Position;

                var offset = 512 - (pos % 512);
                if (offset == 512)
                {
                    offset = 0;
                }

                inputStream.Seek(offset, SeekOrigin.Current);
            }
        }

    }
}