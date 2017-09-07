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

                    if (i + 1 == arr.Length)
                    {
                        Register r2 = new Register();
                        r2.value = current;
                        r2.ammount = currentCounter;
                        Registers.Add(r2);
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
            byte[] bytes = File.ReadAllBytes(CompressedFilePath);
            //string[] lines = File.ReadAllLines(CompressedFilePath);
            //List<string> allLines = new List<string>();
            //string extension = lines[0];
            //for(int i=1;i<lines.Length;i++)
            //{
            //    allLines.Add(Utilities.DeCodeLine(lines[i]));
            //}
            //var d =new DirectoryInfo(CompressedFilePath);
            //File.Create(d.Root+"\\deCom"+d.Name+extension).Dispose();
            //File.WriteAllLines(d.Root + "\\deCom" + d.Name + extension,allLines);
           
        }
    }
}
