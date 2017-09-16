using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Compressor;

namespace Compresion
{
    class Program
    {
        static void Main(string[] args)
        {           
            Huffman h = new Huffman(@"C:\Users\Xico Tzian\Desktop\test.txt");
            h.Compress();
        }
    }
}
