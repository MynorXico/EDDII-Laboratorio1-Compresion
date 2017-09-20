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
            //asi va el nombre -h -c nombre
            //o asi -h -d nombre
            //o asi -r -c nombre 
            // o asi -r -d nombre
            
            string method = args[0];
            string operation = args[1];
            string path = args[2];
            Utilities.Operation(method,operation, path);
        }
    }
}
