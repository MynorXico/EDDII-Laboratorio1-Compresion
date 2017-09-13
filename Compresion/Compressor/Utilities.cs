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
        public static string GetCompressedFileExtension(string FilePath) {
            int lastBackSlashPos = FilePath.LastIndexOf('\\');
            string tmp = FilePath.Substring(lastBackSlashPos + 1);
            return "."+tmp.Split('.')[1];
        }

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

        internal static string GetFileExtension(string filePath)
        {
            var d = new DirectoryInfo(filePath);
            return d.Extension;

        }

        public static string GetFileName(string FilePath)
        {
            int lastBackSlashPos = FilePath.LastIndexOf('\\');
            string tmp = FilePath.Substring(lastBackSlashPos + 1);
            return tmp.Split('.')[0];
        }
        public static void WriteEncodeData(string realPath,string FilePath,byte[]data,int[] amount)
        {
            List<byte> outputBytes = new List<byte>();
            string allLines = string.Empty;
            for(int i=0;i<amount.Length;i++)
            {
                byte s = (byte)amount[i];
                outputBytes.Add(s);
                outputBytes.Add(data[i]);
            }
            File.WriteAllBytes(FilePath, outputBytes.ToArray());

        }
        public static string DeCodeLine( string line)
        {
            string n = string.Empty;
            string decodedLine = string.Empty;
            for(int i=0;i<line.Length;i++)
            {
                string c = line[i].ToString();
                if (int.TryParse(c, out int result))
                {
                    n += c;
                }
                else
                {
                    for(int j=0;j<int.Parse(n);j++)
                    {
                        decodedLine += c;
                    }
                    n = string.Empty;
                }
            }
            return decodedLine;
        }
        
    }
}
