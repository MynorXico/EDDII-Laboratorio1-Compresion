using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace Compressor
{
    class Utilities
    {
        public static int GetNumberOfBytes(string path)
        {
            return File.ReadAllBytes(path).Count();
        }
        public static double CompressionRatio(int sizeAfterCompression, int sizeBeforeCompression)
        {
            return sizeAfterCompression / sizeBeforeCompression;
        }
        public static double CompressionFactor(int sizeAfterCompression, int sizeBeforeCompression)
        {
            return sizeBeforeCompression / sizeAfterCompression;
        }
        public static string GetFolderPath(string FilePath)
        {
            int lastBackSlashPos = FilePath.LastIndexOf('\\');
            return FilePath.Substring(0, lastBackSlashPos + 1);
        }
        public static string GetFileName(string FilePath)
        {
            int lastBackSlashPos = FilePath.LastIndexOf('\\');
            string tmp = FilePath.Substring(lastBackSlashPos + 1);
            return tmp.Split('.')[0];
        }
    }
}
