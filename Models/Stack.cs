using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace DataStructures.Models
{
    public class Stack<T>
    {
        class StackNode<T>
        {
            public T Data;
            public StackNode<T> Next;
        }

        StackNode<T> head;

        public void Push(T data)
        {
            StackNode<T> node = new StackNode<T>();
            node.Data = data;
            node.Next = head;
            head = node;
        }

        public T Pop()
        {
            var d = default(T);

            var temp = head;
            head = head.Next;
            return temp.Data;
        }

        public T Peek()
        {
            return head.Data;
        }
    }
}