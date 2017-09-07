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
            RunLength r = new RunLength(@"C:\Users\Maynor\Documents\CompressorTests\mamania.txt");
            r.Compress();
            //r.Decompress();
        }
    }
}
