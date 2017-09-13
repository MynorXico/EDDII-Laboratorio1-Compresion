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
                    if(r.ammount>255)
                    {
                        int per = r.ammount / 255;
                        int mod = r.ammount % 255;
                        for(int p=0;i<per;i++)
                        {
                            Register perR = new Register();
                            perR.ammount = per;
                            perR.value = r.value;
                            Registers.Add(perR);
                        }
                        Register rper2 = new Register();
                        rper2.ammount = mod;
                        rper2.value = r.value;
                        Registers.Add(rper2);
                    }
                    else
                    {
                        Registers.Add(r);
                    }
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
            int cont = 0;
            var d = new DirectoryInfo(CompressedFilePath);
            File.Create(d.Root + "\\deCom" + d.Name + ".txt").Dispose();
            List<byte> allbytes = new List<byte>();
            for (int i=0;i<bytes.Length;i++)
            {
                if((i+1)%2!=0)//se obtiene la posición par, que corresponden a las cantidades por letra
                {
                    cont = (int)bytes[i];
                }
                if((i+1)%2==0)//se obtienen las posiciones pares, que corresponden al valor de la letra
                {

                    
                    for (int j=0;j<cont;j++)
                    {
                        allbytes.Add(bytes[i]);
                    }
                
                }
                File.WriteAllBytes(d.Root + "\\deCom" + this.FileName + ".txt", allbytes.ToArray());
            }
         

        }
    }
}
