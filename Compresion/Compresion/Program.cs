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
            string method = args[0];
            string filePath = args[1].Substring(2);
            RunLength r = new RunLength(filePath);
            if (method == "-c")
                r.Compress();
            else if (method == "-d")
                r.Decompress();
            else
                Console.WriteLine("No existe el comando indicado.");
        }
    }
}
