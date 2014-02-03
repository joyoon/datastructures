using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace DataStructures.Models
{
    public class HeapPQueue<T> where T : struct
    {
        T[] heap = new T[5];
        int size;
        Comparer<T> comparer;

        public bool isEmpty {
            get
            {
                return size == 0;
            }
        }

        public int Size
        {
            get
            {
                return size;
            }
        }

        public HeapPQueue(Comparer<T> comparer)
        {
            this.comparer = comparer;
        }

        public void Enqueue(T value)
        {
            heap.SetValue(value, size + 1);
            size++;

            //compare the new value to it's parent
            var parentIndex = (size - 1) / 2;
            var newIndex = size;

            if (size > 2)
                BubbleUpRecurse(parentIndex, newIndex, value);

            //check if need to resize
            if (heap.Length == size + 1)
                Resize();
        }

        private void Resize()
        {
            var bufferSize = size + 30;
            var newArray = new T[bufferSize];

            Array.Copy(heap, newArray, heap.Length);
            heap = newArray;
        }

        private void BubbleUpRecurse(int parentIndex, int newIndex, T value)
        {
            var comp = comparer.Compare(heap[parentIndex], value);

            //if parent value is less than new value, return
            if (comp == -1 || comp == 0)
                return;

            //if parent value is greater than new value, swap the values
            else if (comp == 1)
            {
                var temp = heap[parentIndex];
                heap[parentIndex] = value;
                heap[newIndex] = temp;
                newIndex = parentIndex;

                //recalculate parent index
                parentIndex = newIndex / 2;
            }

            BubbleUpRecurse(parentIndex, newIndex, value);
        }

        private static void Heapify(int curIndex, T[] heap, int size, Comparer<T> comparer)
        {
            //check if current node has children
            var leftExists = false;
            var rightExists = false;

            //check if it has left child
            if (curIndex * 2 <= size)
                leftExists = true;

            //check if it has right child
            if (curIndex * 2 + 1 <= size)
                rightExists = true;

            //if no children, return
            if (!leftExists && !rightExists)
                return;
            else
            {
                if (leftExists && !rightExists)
                {
                    //if only left node exists, compare the current node to it
                    var leftChildValue = heap[curIndex * 2];

                    //compare root node to child - swap if child is less than root
                    if (comparer.Compare(leftChildValue, heap[curIndex]) == -1)
                    {
                        var temp = heap[curIndex];
                        heap[curIndex] = leftChildValue;
                        heap[curIndex * 2] = temp;

                        Heapify(curIndex * 2, heap, size, comparer);
                    }
                }
                else if (leftExists && rightExists)
                {
                    //check if left or right is smaller
                    var leftChildValue = heap[curIndex * 2];
                    var rightChildValue = heap[curIndex * 2 + 1];
                    var compResult = comparer.Compare(leftChildValue, rightChildValue);

                    //left child is smaller
                    if (compResult == -1 || compResult == 0)
                    {
                        //compare root node to child - swap if child is greater than root
                        if (comparer.Compare(leftChildValue, heap[curIndex]) == -1)
                        {
                            var temp = heap[curIndex];
                            heap[curIndex] = leftChildValue;
                            heap[curIndex * 2] = temp;

                            Heapify(curIndex * 2, heap, size, comparer);
                        }
                    }
                    //right child is smaller
                    else
                    {
                        //compare root node to child - swap if child is greater than root
                        if (comparer.Compare(rightChildValue, heap[curIndex]) == -1)
                        {
                            var temp = heap[curIndex];
                            heap[curIndex] = rightChildValue;
                            heap[curIndex * 2 + 1] = temp;

                            Heapify(curIndex * 2 + 1, heap, size, comparer);
                        }
                    }
                }
            }
        }

        public T ExtractMin()
        {
            if (size == 0)
                throw new Exception("empty heap");

            var minValue = heap[1];
            heap[1] = heap[size];
            heap[size] = default(T);
            size--;

            Heapify(1, heap, size, comparer);

            return minValue;
        }

        public T Peek()
        {
            if (size > 0)
                return heap[1];
            else
                throw new Exception("empty heap");
        }

        public static HeapPQueue<T> Merge(HeapPQueue<T> pq1, HeapPQueue<T> pq2, Comparer<T> comparer)
        {
            var mergedArr = new T[pq1.size + pq2.size + 1];
            var index = 1;

            while(!pq1.isEmpty)
            {
                mergedArr[index] = pq1.ExtractMin();
                index++;
            }

            while(!pq2.isEmpty)
            {
                mergedArr[index] = pq2.ExtractMin();
                index++;
            }

            var log = (int)Math.Floor(Math.Log(mergedArr.Length, 2));

            //heapify indexes from log base 2 of current index to half of heap size
            MergeHeapify(mergedArr, log, comparer);

            var newPQueue = new HeapPQueue<T>(comparer);
            newPQueue.heap = mergedArr;
            newPQueue.size = mergedArr.Length - 1;

            return newPQueue;
        }

        private static void MergeHeapify(T[] mergedArr, int log, Comparer<T> comparer) {
            if (log == 0)
                return;

            //if initial run, determine max parent node
            var maxParentNode = (int)Math.Pow(2, log) - 1;
            var minParentNode = (int)Math.Pow(2, log - 1);

            //heapify indexes from log base 2 of current index to half of heap size
            for (var i = minParentNode; i <= maxParentNode; i++)
            {
                Heapify(i, mergedArr, mergedArr.Length - 1, comparer);
            }

            log--;

            MergeHeapify(mergedArr, log, comparer);
        }
    }
}