﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Collections;
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
            var d = new DirectoryInfo(realPath);
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
    }
}
