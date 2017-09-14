using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Compressor
{
    public class TreeNode<T>
    {
        private TreeNode<T> Left;
        private TreeNode<T> Right;
        private T Data;
        public TreeNode()
        {
          
        }
        public TreeNode(T data)
        {
            this.Data = data;
        }
        public void SetLeft(TreeNode<T>treeNode)
        {
            Left = treeNode;
        }
        public TreeNode<T>GetLeft()
        {
            return this.Left;
        }
        public void SetRight(TreeNode<T>treeNode)
        {
         
            Right = treeNode;
        }
        public TreeNode<T> GetRight()
        {
            return this.Right;
        }
        public T getData()
        {
            return Data;
        }
        public void SetData(T data)
        {
            this.Data = data;
        }
        

    }
}
