using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Collections;
namespace Compressor
{
    public class Utilities
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
        public static string GetFileExtension(string FilePath)
        {
            int lastBackSlashPos = FilePath.LastIndexOf('\\');
            string tmp = FilePath.Substring(lastBackSlashPos + 1);
            return tmp.Split('.')[1];
        }

        public static void WriteEncodeData(string realPath, string FilePath, byte[] data, int[] amount)
        {
            string allLines = string.Empty;
            List<byte> bs = new List<byte>();
            for (int i = 0; i < data.Length; i++)
            {
                byte[] s = new byte[] { data[i] };
                bs.Add((byte)amount[i]);
                bs.Add((data[i]));

            }
            File.WriteAllBytes(FilePath, bs.ToArray());


        }
        public static string DeCodeLine(string line)
        {
            List<char> AllCharsPerLine = line.ToCharArray().ToList();
            string n = string.Empty;
            string decodedLine = string.Empty;
            foreach (char ch in AllCharsPerLine)
            {
                string c = ch.ToString();
                if (int.TryParse(c, out int result))
                {
                    n += c;
                }
                else
                {
                    for (long j = 0; j < long.Parse(n); j++)
                    {
                        decodedLine += c;
                    }
                    n = string.Empty;
                }
            }


            return decodedLine;
        }

        public static BitArray Reverse(BitArray array)
        {
            int length = array.Length;
            int mid = (length / 2);
            for(int i = 0; i < mid; i++)
            {
                bool bit = array[i];
                array[i] = array[length - i -1];
                array[length - i - 1] = bit;
            }
            return array;
        }
        public static byte ConvertToByte(BitArray array) {
            array = Reverse(array);
            byte[] bytes = new byte[1];
            array.CopyTo(bytes, 0);
            return bytes[0];
        }
        public static string ConvertToBinary(int n){
            string s = Convert.ToString(n, 2);
            return s;
        }
        public static string ByteToBoolArray(byte b){
            string s = ConvertToBinary(b).PadLeft(8,'0');
            return s;
        }
        public static byte stringToByte(string s)
        {
            return Convert.ToByte(s, 2);
        }
        public static void Operation(string method,string operation,string filePath)
        {
            Huffman h = null;
            RunLength r = null;
            if(!File.Exists(filePath))
            {
                Console.WriteLine("The file doesn´t exist");
                return;
            }
            else
            {
                if(operation=="-c")
                {
                    if (method == "-h")//comprime con huffman
                    {
                        h = new Huffman(filePath);
                        h.Compress();
                        
                    }
                    else if (method == "-r")//comprime con RLE
                    {
                        r = new RunLength(filePath);
                        r.Compress();//Hay que meterle la funcion showStatistics
                        
                    }
                    else
                    {
                        Console.WriteLine("The method is not valid");
                        return;
                    }
                }
                else
                {
                    Console.WriteLine("The operaton must be {0} ","-c");
                    return;
                }
            }
           
        }
        public static void decompress(string path)
        {
            string[] dCompressMode = path.Split('.');
            if (dCompressMode.Length == 4)//el path del archivo cumplia con la sintaxis correcta
            {
                if (dCompressMode[2] == "h")
                {
                    Huffman h = new Huffman(path);// solo se utiliza esta dirección para instanciar el objeto
                    h.HuffmanDeCompress(path);//se usa la direccion del archivo comprimido
                    Console.WriteLine("Succes!");
                }
                else if(dCompressMode[2]=="r")//se debe tratar de una compresion con rle
                {
                    RunLength r = new RunLength(path);
                    r.Decompress();
                    Console.WriteLine("Succes!");
                }
                else
                {
                    Console.WriteLine("The fullname of the file must have {0} or {1}",".r",".h");
                    return;
                }
            }
            else
            {
                Console.WriteLine("The path {0} is not valid",path);
                return;
            }
        }
        public static void showStatistics(string compressedFilePath,string originalPath)
        {
            var d0 = new FileInfo(originalPath);
            var d1 = new FileInfo(compressedFilePath);
            double compressionRatio =  ((double)d1.Length / (double)d0.Length);
            double compressionFactor =  (((double)d0.Length / (double)d1.Length));
            double savingPercentage = (((double)d0.Length - (double)d1.Length) / (double)d0.Length) * 100;
            Console.WriteLine("Statistics\n\n");
            Console.WriteLine("*Original Size {0}",d0.Length);
            Console.WriteLine("*Final Size {0}",d1.Length);
            Console.WriteLine("*Compression Ratio: {0}",Math.Round(compressionRatio,2));
            Console.WriteLine("*Compression Factor: {0}",Math.Round(compressionFactor,2));
            Console.WriteLine("*Savin Percentage: {0}%",Math.Round(savingPercentage,2));
        }

        public static string GetCompressedFileExtension(string FilePath)
        {
            int lastBackSlashPos = FilePath.LastIndexOf('\\');
            string tmp = FilePath.Substring(lastBackSlashPos + 1);
            return "." + tmp.Split('.')[1];
        }

        public static double SavingPercentage(double sizeAfterCompression, double sizeBeforeCompression)
        {
            return Math.Round((((sizeAfterCompression - sizeBeforeCompression) / sizeAfterCompression) * 100), 5);
        }

    }
}
