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
            RunLength r = new RunLength(args[0]);
            r.Compress();
            r.Decompress();
        }
    }
}
