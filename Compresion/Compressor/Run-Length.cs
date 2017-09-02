using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace Compressor
{
    public class RunLength : ICompressor
    {
        struct Register{
            public byte value;
            public int ammount;
        }

        private string path;

        public RunLength(string path)
        {
            this.path = path;
        }

        public void Compress()
        {
            byte[] arr = File.ReadAllBytes(this.path);
            byte current = arr[0];
            int currentCounter = 0;

            List<Register> Registers = new List<Register>();
            for(int i = 0; i < arr.Length; i++)
            {
                if(arr[i] == current)
                {
                    currentCounter++;
                    if(i+1 == arr.Length)
                    {
                        Register r = new Register();
                        r.value = current;
                        r.ammount = currentCounter;
                        Registers.Add(r);
                    }
                }
                else
                {
                    Register r = new Register();
                    r.value = current;
                    r.ammount = currentCounter;
                    Registers.Add(r);

                    current = arr[i];
                    currentCounter = 1;
                }
            }

            Console.ReadLine();
        }

        public void Decompress()
        {
           
        }
    }
}
