using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace DataStructures.Models
{
    public class BST
    {
        Node head;

        public class Node
        {
            public int Key;
            public Node Left;
            public Node Right;

            public Node(int data)
            {
                Key = data;
            }
        }

        public BST()
        {
            head = null;
        }

        public string PrintTree()
        {
            return PrintTree(head);
        }

        private string PrintTree(Node node)
        {
            if (node == null)
                return "";

            return PrintTree(node.Left) + node.Key + " " + PrintTree(node.Right);
        }

        public string PrintPostOrder()
        {
            return PrintPostOrder(head);
        }

        private string PrintPostOrder(Node node)
        {
            if (node == null)
                return "";

            return PrintPostOrder(node.Left) + PrintPostOrder(node.Right) + node.Key;
        }

        public int hasPathSum(int sum)
        {
            return hasPathSum(head, sum) == 1 ? 1 : 0;
        }

        private int hasPathSum(Node node, int sum)
        {
            if (node.Left == null && node.Right == null)
                return sum - node.Key == 0 ? 1 : 0;

            var leftPathSum = 0;
            var rightPathSum = 0;

            if (node.Left != null)
                leftPathSum = hasPathSum(node.Left, sum - node.Key);

            if (node.Right != null)
                rightPathSum = hasPathSum(node.Right, sum - node.Key);

            if (leftPathSum == 1 || rightPathSum == 1)
                return 1;
            else
                return 0;
        }

        public void printPaths()
        {
            printPaths(head);
        }

        private void printPaths(Node node)
        {
            if (node != null)
            {
                int[] path = new int[10];
                printPathsRecur(node, path, 0);
            }
        }

        private void printPathsRecur(Node node, int[] path, int pathLength)
        {
            if(node.Left == null && node.Right == null) {
                path[pathLength] = node.Key;

                var pathString = path.Take(pathLength + 1);
                return;
            }

            if (node != null)
            {
                path[pathLength] = node.Key;
                pathLength++;

                if (node.Left != null)
                    printPathsRecur(node.Left, path, pathLength);

                if (node.Right != null)
                    printPathsRecur(node.Right, path, pathLength);
            }
        }

        public void Mirror()
        {
            Mirror(head);
        }

        private void Mirror(Node node)
        {
            if (node != null)
            {
                Node temp;

                temp = node.Left;
                node.Left = node.Right;
                node.Right = temp;

                if (node.Left != null)
                    Mirror(node.Left);

                if (node.Right != null)
                    Mirror(node.Right);
            }
        }

        public static int sameTree(Node a, Node b)
        {
            if (a != null && b != null)
            {
                if (a.Key != b.Key)
                    return 0;
            }

            var leftSame = 1;
            var rightSame = 1;

            if (a.Left != null && b.Left != null)
            {
                leftSame = sameTree(a.Left, b.Left);

                if (leftSame == 0)
                    return 0;
            }
            else
            {
                if (!(a.Left == null && b.Left == null))
                    return 0;
            }

            if (a.Right != null && b.Right != null)
            {
                rightSame = sameTree(a.Right, b.Right);

                if (rightSame == 0)
                    return 0;
            }
            else
            {
                if (!(a.Left == null && b.Left == null))
                    return 0;
            }

            return 1;
        }

        public static int countTrees(int numKeys)
        {
            return countTreesRecur(numKeys);
        }

        private static int countTreesRecur(int numKeys)
        {
            if (numKeys == 0)
                return 1;

            if (numKeys == 1)
                return 1;

            if (numKeys == 2)
                return 2;

            if (numKeys == 3)
                return 5;

            int numTreesOne = 0;

            return countTreesRecur(numKeys - 1) +
                countTreesRecur(numKeys - 2) +
                countTreesRecur(2) * countTreesRecur(numKeys - 3) +
                (countTreesRecur(3) * countTreesRecur(numKeys - 4));
        }

        public void doubleTree()
        {
            doubleTree(head);
        }

        private void doubleTree(Node node)
        {
            //create new node
            Node duplicate = new Node(node.Key);
            duplicate.Left = node.Left;
            duplicate.Right = node.Right;

            node.Left = duplicate;

            if (duplicate.Left != null)
                doubleTree(duplicate.Left);

            if (duplicate.Right != null)
                doubleTree(duplicate.Right);
        }

        public int Size()
        {
            return Size(head);
        }

        private int Size(Node node)
        {
            if (node == null)
                return 0;
            else
                return Size(node.Left) + Size(node.Right) + 1;
        }

        public int MinValue()
        {
            return MinValue(head);
        }

        private int MinValue(Node node)
        {
            if (node.Left == null)
                return node.Key;

            return MinValue(node.Left);
        }

        public int MaxDepth()
        {
            return MaxDepth(head);
        }

        private int MaxDepth(Node node)
        {
            var leftDepth = 0;
            var rightDepth = 0;

            if (node == null)
                return 0;
            else
            {
                //get max depth of left tree
                leftDepth = MaxDepth(node.Left) + 1;

                //get max depth of left tree
                rightDepth = MaxDepth(node.Right) + 1;
            }

            return leftDepth > rightDepth ? leftDepth : rightDepth;
        }

        public void Insert(int data)
        {
            head = Insert(head, data);
        }

        private Node Insert(Node node, int value)
        {
            //if head is empy, just insert the node
            if (node == null)
                return new Node(value);
            else
            {
                //head is not empty

                //compare current key value to new node's key value
                if (value <= node.Key)

                    //new node key is less than current node key
                    node.Left = Insert(node.Left, value);
                else
                    //new node key is greater than current node key
                    node.Right = Insert(node.Right, value);
            }

            return node;
        }

        public bool Lookup(int value)
        {
            return Lookup(head, value);
        }

        private bool Lookup(Node head, int value)
        {
            if (head == null)
                return false;
            else
            {
                //compare value to head value
                if (value == head.Key)
                    return true;

                //compare lookup value to current node key value
                if (value <= head.Key)
                    return Lookup(head.Left, value);
                else
                    return Lookup(head.Right, value);
            }
        }
    }
}