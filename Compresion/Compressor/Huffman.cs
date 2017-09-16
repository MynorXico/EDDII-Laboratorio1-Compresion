using System;
using System.Collections.Generic;
using System.Collections;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace Compressor
{
    public class Huffman:ICompressor
    {
        public struct Register
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
        Dictionary<byte,Register> dictionary = new Dictionary<byte,Register>();
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
            var auxList = new List<Register>();
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
                    auxList.Add(aux);
                    aux = Registers[i];
                    o = 1;
                }
            }
            aux.ammount = o;
            nodes.Add(new TreeNode<Register>(aux));
            auxList.Add(aux);

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
            string binaryCode = string.Empty;
            for(int i=0;i<auxList.Count; i++)
            {
                Transverse(m,  binaryCode,auxList[i]);
            }
            BitArray b = null;

            // Listado de boolean con el mensaje final
            List<bool> bls = new List<bool>();

            bool[] encodedDictionary = EncodedDictionary(dictionary); // bool[]
            byte dictionaryLength = (byte)encodedDictionary.Length;
            bool[] binaryDictionaryLength = 
                boolConverter(Utilities.ByteToBoolArray(dictionaryLength).ToCharArray()); // bool[]

            bls.AddRange(binaryDictionaryLength); // Length of dictionary in bits
            bls.AddRange(encodedDictionary); // Adds the dictionary

            for (int i=0;i<arr.Length;i++)
            {
                Register auxRegister = dictionary[arr[i]];
                dictionary.TryGetValue(arr[i], out auxRegister);
                char[] s =auxRegister.binary.ToCharArray();
                bool[] bts = boolConverter(s);
                bls.AddRange(bts);
            }
            
            int AddedBits = 0;
            while(bls.Count % 8 != 0)
            {
                bls.Add(false);
                AddedBits++;
            }
            

            b = new BitArray(bls.ToArray());//arreglo de bits con el mensaje

            // Convierte A Bytes
            List<byte> byteOutputList = new List<byte>();
            for(int i = 0; i < b.Length; i+=8){
                bool[] Bool = new bool[8];
                for(int j = i; (j-i) < 8; j++){
                   Bool[j-i] = bls[j];
                }
                BitArray bitArray = new BitArray(Bool);
                byteOutputList.Add(Utilities.ConvertToByte(bitArray));
            }
            
            

            
            
            
           

            File.WriteAllBytes(@"C:\Users\Xico Tzian\Desktop\output.txt",byteOutputList.ToArray());
        }
        private bool[] boolConverter(char[]list)
        {
            bool[] b = new bool[list.Length];
            for (int i = 0; i <b.Length;i++)
            {
                if(list[i] == '1')
                {
                    b[i] = true;
                }
                else
                {
                    b[i]=false;
                }
               
            }
            return b;
        }
        private TreeNode<Register> CreateTree(TreeNode<Register> A, TreeNode<Register> B)
        {
            TreeNode<Register> father = new TreeNode<Register>();
            if (A.getData().ammount >= B.getData().ammount)
            {
                father.SetLeft(B);
                father.SetRight(A);
            }
            else if (A.getData().ammount < B.getData().ammount)
            {
                father.SetLeft(A);
                father.SetRight(B);
            }
            var r = new Register();
            r.ammount = A.getData().ammount + B.getData().ammount;
            father.SetData(r);
            return father;
            
        }
        
        private void Transverse(TreeNode<Register>node, string binaryCode,Register auxRegister)
        {
            if(node.GetLeft()==null&&node.GetRight()==null)
            {
                if(node.getData().value==auxRegister.value)
                {
                    Register aux = new Register();
                    aux.binary = binaryCode;
                    aux.ammount = node.getData().ammount;
                    aux.value = node.getData().value;
                    node.SetData(aux);
                    dictionary.Add(aux.value,aux);
                   
                }
            }
            else
            {
                if (node.GetLeft() != null)
                {
                    string leftPath = string.Empty;
                    leftPath = binaryCode + "0";
                    Transverse(node.GetLeft(), leftPath, auxRegister);
                    
                }
                if (node.GetRight() != null)
                {
                    string rightPath = string.Empty;
                    rightPath = binaryCode+ "1";
                    Transverse(node.GetRight(), rightPath, auxRegister);
                  
                }
                if (node.GetLeft() == null||node.GetRight()==null)
                {
                    return;
                }
            }
            
        }
        public void Decompress()
        {
            throw new NotImplementedException();
        }

        public bool[] EncodedDictionary(Dictionary<byte, Register> Dictionary){
            bool[] Output = new bool[3];
            
            List<bool> BinaryOutput = new List<bool>();

            StringBuilder BinaryString = new StringBuilder();

            foreach(KeyValuePair<byte, Register> register in Dictionary){
                Register ActualRegister = register.Value;
                string ActualValue = Utilities.ConvertToBinary(ActualRegister.value); // 8bits
                string ActualBinary = ActualRegister.binary; // Length can vary.
                string binaryLength = Utilities.ConvertToBinary(ActualBinary.Length).PadLeft(8,'0'); // 8bits
                BinaryString.Append(ActualValue);
                BinaryString.Append(binaryLength);
                BinaryString.Append(ActualBinary);
            }
            char[] s = BinaryString.ToString().ToCharArray();
            bool[] bts = boolConverter(s);

            return bts;
        }    
    }
}
