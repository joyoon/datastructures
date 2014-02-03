using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace DataStructures.Models
{
    public class LinkedList<T>
    {
        public class Node<T>
        {
            public Node<T> Prev { get; set; }
            public Node<T> Next { get; set; }
            public T obj { get; set; }
        }

        Node<T> head;
        int size;

        public void InsertHead(T value)
        {
            var newNode = new Node<T>();
            newNode.obj = value;

            if (head == null)
                head = newNode;
            else
            {
                newNode.Next = head;
                head.Prev = newNode;
                head = newNode;
            }

            size++;
        }

        public void InsertBefore(Node<T> node, T value)
        {
            if (head == null)
                return;
            else
            {
                //create a new node
                var newNode = new Node<T>();
                newNode.obj = value;

                //set the new node's next pointer to the insertion point
                newNode.Next = node;

                //set the new node's previous pointer to the insertion point's previous pointer
                newNode.Prev = node.Prev;

                //set the next pointer of the node in front of the new node to the new node
                node.Prev.Next = newNode;

                //set the insertion point node's previous pointer to the new node
                node.Prev = newNode;

                size++;
            }
        }

        public void InsertAfter(Node<T> node, T value)
        {
            if (head == null)
                return;
            else
            {
                var newNode = new Node<T>();
                newNode.obj = value;

                //set the new node's next pointer to the insertion point's next pointer
                newNode.Next = node.Next;

                //set the new node's previous pointer to the insertion point
                newNode.Prev = node;

                //set the previous pointer of the node after insertion point to the new node
                if (node.Next != null)
                    node.Next.Prev = newNode;

                //set the insertion point's next pointer to the new node
                node.Next = newNode;

                size++;
            }
        }

        public Node<T> First()
        {
            return head;
        }

        public Node<T> RemoveFirst()
        {
            if (head == null)
                throw new InvalidOperationException("");

            var temp = head;
            head = head.Next;
            size--;

            return temp;
        }

        public Node<T> Find(T val)
        {
            Node<T> temp = head;

            while (temp != null && temp.obj.Equals(val))
            {
                temp = head.Next;
            }

            return temp;
        }

        public int Size()
        {
            return size;
        }
    }
}