using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace DataStructures.Models
{
    public class BinomialHeapPQueue
    {
        List<Node> heap = new List<Node>();

        public void Enqueue(string word)
        {
            Node node = new Node(word);

            if (heap.Count == 0)
                heap.Add(node);
            else
            {
                EnqueueRecur(0, node);

                //set the root to blank string
                var root = heap[0];
                root.elem = "";
            }
        }

        private void EnqueueRecur(int index, Node node) {
            //merge the node with the current order-0 node to create order-1 tree
            if (!string.IsNullOrEmpty(node.elem))
            {
                var newTree = heap[index];
                newTree.children.Add(node);

                EnqueueRecur(index + 1, newTree);
            }
        }

        public string ExtractMin()
        {
            return "";
        }

        public BinomialHeapPQueue Merge(BinomialHeapPQueue q1, BinomialHeapPQueue q2)
        {
            return null;
        }
    }

    public struct Node
    {
        public string elem;
        public List<Node> children;

        public Node(string elem)
        {
            this.elem = elem;
            children = new List<Node>();
        }
    }
}