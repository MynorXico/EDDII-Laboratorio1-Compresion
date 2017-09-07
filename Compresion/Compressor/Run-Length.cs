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
        struct Register {
            public byte value;
            public int ammount;
        }

        private string FilePath;
        private string CompressedFilePath;
        private string FolderPath;
        private string FileName;

        public RunLength(string FilePath)
        {
            this.FilePath = FilePath;
            this.FolderPath = Utilities.GetFolderPath(FilePath);
            this.FileName = Utilities.GetFileName(FilePath);
            CompressedFilePath = string.Empty;
        }

        public void Compress()
        {
            
            byte[] arr = File.ReadAllBytes(this.FilePath);
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
                        Register r = new Register
                        {
                            value = current,
                            ammount = currentCounter
                        };
                        Registers.Add(r);
                    }
                }
                else
                {
                   
                    Register r = new Register
                    {
                        value = current,
                        ammount = currentCounter
                    };
                    Registers.Add(r);

                    current = arr[i];
                    currentCounter = 1;
                    if (i + 1 == arr.Length)
                    {
                        Register c = new Register
                        {
                            value = current,
                            ammount = currentCounter
                        };
                        Registers.Add(c);
                    }
                }
            }
            byte[] outputBytes =  GetOutputBytes(Registers);
            
            Utilities.WriteEncodeData(FilePath,FolderPath + FileName + ".rlex",outputBytes,getOutputAmount(Registers));
            CompressedFilePath = FolderPath + FileName + ".rlex";
        }
        private byte[] GetOutputBytes(List<Register> registers)
        {
            List<byte> allBytes = new List<byte>();
            foreach(Register r in registers)
            {
                allBytes.Add(r.value);
            }
            return allBytes.ToArray();
        }
        private int[]getOutputAmount(List<Register>registers)
        {
            List<int> amount = new List<int>();
            foreach(Register r in registers)
            {
                amount.Add((byte)r.ammount);
            }
            return amount.ToArray();
        }
        public void Decompress()
        {
            List<string> lines = File.ReadAllLines(CompressedFilePath).ToList();
            List<string> allLines = new List<string>();
            lines.RemoveAt(0);
            for(int i=0;i<lines.Count;i++)
            {
                allLines.Add(Utilities.DeCodeLine(lines[i]));
            }
            var d =new DirectoryInfo(CompressedFilePath);
            File.Create(d.Root+"\\prueba.txt").Dispose();
            File.WriteAllLines(d.Root + "\\prueba.txt",allLines);
           
        }
    }
}
