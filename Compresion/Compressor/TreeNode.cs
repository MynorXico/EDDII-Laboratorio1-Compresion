using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Compressor
{
    public class TreeNode<T>where T:IComparable
    {
        private TreeNode<T> Left;
        private TreeNode<T> Right;
        private T Data;
        public TreeNode()
        {
            Left = new TreeNode<T>();
            Right = new TreeNode<T>();
        }
        public TreeNode(T data)
        {
            this.Data = data;
            Left = new TreeNode<T>();
            Right = new TreeNode<T>();
        }
        public void SetLeft(TreeNode<T>treeNode)
        {
            Left = new TreeNode<T>();
            Left = treeNode;
        }
        public TreeNode<T>GetLeft()
        {
            return this.Left;
        }
        public void SetRight(TreeNode<T>treeNode)
        {
            Right = new TreeNode<T>();
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
