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
        private string path;

        public RunLength(string path)
        {
            this.path = path;
        }

        public void Compress()
        {
            string[] lines = File.ReadAllLines(path);

            List<StringBuilder> OutputLines = new List<StringBuilder>();


            for (int i = 0; i < content.Length; i++)
            {
                string aux = lines[i][0].ToString();
                for (int j = 0; j < lines[i].Length; j++)
                {
                    if (lines[i][j].ToString() == aux)
                    {
                        auxCount++;
                    }
                    else
                    {
                        s.Append(auxCount);
                        s.Append(aux);

                        aux = lines[i][j].ToString();
                        auxCount = 0;
                    }                   
                }
                OutputLines.Add(s);
            }

            
        }

        public void Decompress()
        {
            
        }
    }
}
