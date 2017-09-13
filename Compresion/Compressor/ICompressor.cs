using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Compressor
{
    interface ICompressor
    {
        bool Compress();
        bool Decompress();
        void ShowStatistics();
    }
}
