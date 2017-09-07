using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace Compressor
{
    public class Huffman:ICompressor
    {
        struct Register
        {
            public byte value;
            public int ammount;
        }
        private string FilePath;
        public Huffman(string FilePath)
        {
            this.FilePath = FilePath;
        }

        public void Compress()
        {
            byte[] arr = File.ReadAllBytes(this.FilePath);
            byte current = arr[0];
            int currentCounter = 0;

            List<Register> Registers = new List<Register>();
            for (int i = 0; i < arr.Length; i++)
            {
                if (arr[i] == current)
                {
                    currentCounter++;
                    if (i + 1 == arr.Length)
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
            int a = 0;
            Registers = Registers.OrderBy(x => x.value).ToList();
            int counter = 0;
            List<int> allAmounnts = new List<int>();
            List<string> allValues = new List<string>();
            byte aux = Registers[0].value;
            while (counter < Registers.Count)
            {
                if(Registers[counter].value!=aux)
                {
                    allAmounnts.Add(a);
                    allValues.Add(Encoding.Default.GetString(new byte[] { aux}));
                    a = 1;
                    aux = Registers[counter].value;
                }
                else
                {
                    
                    a++;
                }
                counter++;
            }
            List<TreeNode<int>> allNodes = new List<TreeNode<int>>();
            for(int i=0;i<allValues.Count;i++)
            {
                TreeNode<int> node = new TreeNode<int>(allAmounnts[i]);
                allNodes.Add(node);
            }
            bool flag = false;
            while(flag)
            {
                TreeNode<int> A = allNodes[allNodes.Count-1];
                TreeNode<int> B = allNodes[allNodes.Count - 2];
                allNodes.Remove(A);
                allNodes.Remove(B);
                allNodes.Add(CreateTree(A, B));
                if(allNodes.Count==1)
                {
                    flag = true;
                }
            }
            TreeNode<int> m = allNodes[0];

            
        
        }

        public void Decompress()
        {
            throw new NotImplementedException();
        }

        private TreeNode<int> CreateTree(TreeNode<int>A, TreeNode<int>B)
        {
            TreeNode<int> father = new TreeNode<int>();
            father.SetLeft(A);
            father.SetRight(B);
            father.SetData(A.getData() + B.getData());
            return father;
        }
    }
}
