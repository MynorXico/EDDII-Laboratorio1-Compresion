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
    }
}
