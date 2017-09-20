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
                if (r.Compress())
                    Console.WriteLine("Compreso exitosamente.\nEl archivo compreso se encuentra en la misma ruta del archivo original.");
                else
                    Console.WriteLine("Ocurrió un error al comprimir el archivo especificado.");
            else if (method == "-d")
                if (r.Decompress())
                    Console.WriteLine("Descompreso exitosamente.\nEl archivo descompreso, se encuentra en la carpeta Debug del proyecto.");
                else
                    Console.WriteLine("Ocurrió un error al descomprimir el archivo especificado.");
            else
                Console.WriteLine("No existe el comando indicado.");

            Console.WriteLine("Presione una tecla para salir...");
            Console.ReadKey();
        }
    }
}
