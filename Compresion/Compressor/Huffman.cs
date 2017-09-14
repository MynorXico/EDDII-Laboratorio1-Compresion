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
            public string binary;
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

                    if (i + 1 == arr.Length)
                    {
                        Register r2 = new Register();
                        r2.value = current;
                        r2.ammount = currentCounter;
                        Registers.Add(r2);
                    }
                }
            }
            Registers = Registers.OrderByDescending(x => x.value).ToList();
            int o = 0;
            var aux = Registers[0];
            List<TreeNode<Register>> nodes = new List<TreeNode<Register>>();
            for (int i = 0; i < Registers.Count; i++)
            {
                if (Registers[i].value == aux.value)
                {
                    o++;
                }
                else
                {
                    aux.ammount = o;
                    nodes.Add(new TreeNode<Register>(aux));
                    aux = Registers[i];
                    o = 1;
                }
            }
            aux.ammount = o;
            nodes.Add(new TreeNode<Register>(aux));
            nodes = nodes.OrderByDescending(x => x.getData().ammount).ToList();
            bool flag = false;
            while (flag == false)
            {

                TreeNode<Register> A = nodes[nodes.Count - 1];
                nodes.Remove(A);
                TreeNode<Register> B = nodes[nodes.Count - 1];
                nodes.Remove(B);
                nodes.Add(CreateTree(A, B));
                nodes = nodes.OrderByDescending(x => x.getData().ammount).ToList();
                if (nodes.Count == 1)
                {
                    flag = true;
                }
            }
            TreeNode<Register> m = nodes[0];
        }
        public void Decompress()
        {
            throw new NotImplementedException();
        }

        private TreeNode<Register> CreateTree(TreeNode<Register>A, TreeNode<Register>B)
        {
            TreeNode<Register> father = new TreeNode<Register>();
            if(A.getData().ammount>=B.getData().ammount)
            {
                father.SetLeft(B);
                father.SetRight(A);
            }
            else if(A.getData().ammount<B.getData().ammount)
            {
                father.SetLeft(A);
                father.SetRight(B);
            }
            var r = new Register();
            r.ammount = A.getData().ammount + B.getData().ammount;
            father.SetData(r);
            return father;
        }
    }
}
