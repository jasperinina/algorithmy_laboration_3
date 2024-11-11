using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace lab3.Logic
{
    public class TreeNode
    {
        public object Data; // ключ/данные
        public TreeNode Left; // указатель на левого потомка
        public TreeNode Right; // указатель на правого потомка

        public TreeNode(object data)
        {
            Data = data;
            Left = null;
            Right = null;
        }
    }
    public class BinaryTree
    {
        public string PreorderPrint(TreeNode root)
        {
            if (root == null)
            {
                return "* ";
            }
            return root.Data + " " + PreorderPrint(root.Left) + PreorderPrint(root.Right);
        }
        public string InorderPrint(TreeNode root)
        {
            if (root == null)
            {
                return "* ";
            }
            return InorderPrint(root.Left) + root.Data + " " + InorderPrint(root.Right);
        }

        public string PostorderPrint(TreeNode root)
        {
            if (root == null)
            {
                return "* ";
            }
            return PostorderPrint(root.Left) + PostorderPrint(root.Right) + root.Data + " ";
        }
    }
}
