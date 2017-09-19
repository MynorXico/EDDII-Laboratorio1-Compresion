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
            

            File.WriteAllBytes(@"C:\output.txt", byteOutputList.ToArray());
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
        public void HuffmanDeCompress(string ouputFilePath)
        {
            byte[] compressedFileBytes = File.ReadAllBytes(ouputFilePath);
            StringBuilder sb = new StringBuilder();
            for(int i=0;i<compressedFileBytes.Length;i++)
            {
                sb.Append(Utilities.ByteToBoolArray(compressedFileBytes[i]));
               
            }
            int D = Convert.ToInt32(sb.ToString().Substring(0,8),2);
            var binaryDictionary = sb.ToString(8,D);
            string aux = string.Empty;
            Dictionary<byte, Register> DecompressedDictionary = new Dictionary<byte, Register>();
            for(int i=0;i<binaryDictionary.Length;i++)
            {
                if((i+1)%16==0)//se supone que tomo los el valor y la cantidad de bits que ocupa el binario de ese valor
                {
                    string binaryValue = aux.Substring(0,8); 
                    int binaryLength = Convert.ToInt32(aux.Substring(8,8),2);
                    string binaryRepresentation = binaryDictionary.Substring(i+1,binaryLength);
                    Register r = new Register();
                    r.binary = binaryRepresentation;
                    r.value = Utilities.stringToByte(binaryValue);
                    DecompressedDictionary.Add(r.value, r);
                    binaryDictionary = binaryDictionary.Substring(i + binaryLength);
                    aux = string.Empty;
                    i = 0;
                    if(binaryDictionary==null)
                    {
                        break;
                    }
                }
                else
                {
                    aux += binaryDictionary[i];
                }
            }
            var a = sb.ToString();
            //int D = (compressedFileBytes[0]);//numero de bits que ocupa el diccionario
            //List<BitArray> b = new List<BitArray>();
            //for(int i=1;i<compressedFileBytes.Length;i++)
            //{
            //    b.Add(new BitArray(compressedFileBytes[i]));
            //}
            //List<bool> mainBoolList = new List<bool>();
            //foreach (BitArray arrbits in b)
            //{
            //    List<bool> boolList = new List<bool>();
            //    for(int i=0;i<arrbits.Count;i++)
            //    {
            //        boolList.Add((bool)arrbits[i]);
            //    }
            //    mainBoolList.AddRange(boolList);
            //}




            //var bits = new BitArray(compressedFileBytes);
            //var dictionaryBits = new BitArray(D+8);



            //for(int i=0;i<D+8;i++)
            //{
            //    dictionaryBits[i] = bits[i];
            //}

            //for(int i=0;i<dictionaryBits.Length;i++)
            //{

            //}
            //byte[] dictionaryBytes = new Byte[dictionaryBits.Length];
            //dictionaryBits.CopyTo(dictionaryBytes, 0);



        }
        public  void Decompress()
        {
           
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
