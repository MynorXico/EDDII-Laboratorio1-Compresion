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
            //SINTAXIS PARA COMPRESIÓN
            //Método -c nombre del archivo
            //Ejemplo h -c imagen.jpg o r -c imagen.jpg

            //SINTAXIS PARA DESCOMPRESIÓN
            //-d nombre del archivo compreso con extensión .comp
            //Ejmplo: -d imagen.jpg.r.comp o -d imagen.jpg.h.comp

            string method = string.Empty;
            string operation = string.Empty;
            string path = string.Empty;
            if (args.Length==2)// se supone que solo viene el -d y el path
            {
                operation = args[0];
                path = args[1];
                if(operation=="-d")
                {
                    //aqui se elije el método a utilizar para descomprimir
                    Utilities.decompress(path);
                }
                else
                {
                    Console.WriteLine("The method {0} is not valid",operation);
                    return;
                }
            }
            else if(args.Length==3)//se supone que viene el metodo, la operación y la dirección del archivo
            {
                method = args[0];
                operation = args[1];
                path = args[2];
                Utilities.Operation(method, operation, path);
            }
            else
            {
                StringBuilder sb = new StringBuilder();
                for(int i=0;i<args.Length;i++)
                {
                    sb.Append(args[i]);
                }
                Console.WriteLine("The sintax of the pettion: {0} is not valid",sb.ToString());
                return;
            }
          
          
        }
    }
}
