using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace DataStructures.Models
{
    public class PQueue<T>
    {
        LinkedList<T> queue;
        Comparer<T> comparer;

        public PQueue(Comparer<T> comparer)
        {
            queue = new LinkedList<T>();
            this.comparer = comparer;
        }

        public void Enqueue(T value)
        {
            //to find insertion point, iterate through list and compare value to node's value
            var insertionPoint = queue.First();
            var comp = 1;
            var nextComp = 1;

            if (insertionPoint == null)
                queue.InsertHead(value);
            else
            {
                comp = comparer.Compare(insertionPoint.obj, value);

                if (comp == 1)
                    queue.InsertHead(value);
                else
                {
                    //-------------------- /
                    //find insertion point /
                    //-------------------- /
                    if (insertionPoint.Next != null)
                        nextComp = comparer.Compare(insertionPoint.Next.obj, value);

                    //while new node value is greater than current node value and next node value, move to next node
                    while (insertionPoint.Next != null && comp != 1 && nextComp != 1)
                    {
                        insertionPoint = insertionPoint.Next;
                        comp = comparer.Compare(insertionPoint.obj, value);

                        if (insertionPoint.Next != null)
                            nextComp = comparer.Compare(insertionPoint.Next.obj, value);
                        else
                            nextComp = 1;
                    }

                    queue.InsertAfter(insertionPoint, value);
                }
            }
        }

        public T ExtractMin()
        {
            var temp = queue.RemoveFirst();

            return temp.obj;
        }

        public T Peek()
        {
            return queue.First().obj;
        }

        public int Size()
        {
            return queue.Size();
        }

        public static PQueue<T> Merge(PQueue<T> pq1, PQueue<T> pq2, Comparer<T> comparer)
        {
            PQueue<T> newQueue = new PQueue<T>(comparer);
            MergeRecur(newQueue, pq1, pq2, comparer);

            return newQueue;
        }

        private static void MergeRecur(PQueue<T> origQueue, PQueue<T> pq1, PQueue<T> pq2, Comparer<T> comparer)
        {
            if ((pq1.Size() == 0 && pq2.Size() == 0))
                return;

            if ((pq1.Size() == 0 && pq2.Size() > 0))
            {
                origQueue.Enqueue(pq2.ExtractMin());
                MergeRecur(origQueue, pq1, pq2, comparer);
                return;
            }

            if (pq2.Size() == 0 && pq1.Size() > 0)
            {
                origQueue.Enqueue(pq1.ExtractMin());
                MergeRecur(origQueue, pq1, pq2, comparer);
                return;
            }

            T pq1Min = default(T);
            T pq2Min = default(T);

            if (pq1.Size() > 0 && object.Equals(pq1Min, default(T)))
                pq1Min = pq1.Peek();

            if (pq2.Size() > 0 && object.Equals(pq2Min, default(T)))
                pq2Min = pq2.Peek();

            var comp = comparer.Compare(pq1Min, pq2Min);
            var itemToAdd = (comp == -1) ? pq1Min : pq2Min;

            if (comp == -1)
                pq1.ExtractMin();
            else
                pq2.ExtractMin();

            origQueue.Enqueue(itemToAdd);

            MergeRecur(origQueue, pq1, pq2, comparer);
        }
    }
}