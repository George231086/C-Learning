using System;
using System.Collections.Generic;
using System.Linq;

namespace CSharpLearning
{
    /// <summary>
    /// Basic min heap using generics. A simple modification of the the max heap pseudocode
    /// in "Introduction to Algorithms", Cormen, Leiserson, Rivest and Stein. Includes static method to
    /// heap sort an array.
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class MinHeap<T> where T : IComparable
    {
        private T[] array;
        private int length;
        private int heapSize;

        public MinHeap(T[] array)
        {
            this.array = array;
            length = array.Length;
            BuildMinHeap();
        }

        public void BuildMinHeap()
        {
            heapSize = length;
            for (int i = length / 2; i >= 0; i--)
            {
                MinHeapify(i);
            }
        }

        public void MinHeapify(int i)
        {
            var l = Left(i);
            var r = Right(i);
            var smallest = i;
            if (l < heapSize && array[l].CompareTo(array[i]) < 0)
            {
                smallest = l;
            }
            if (r < heapSize && array[r].CompareTo(array[smallest]) < 0)
            {
                smallest = r;
            }
            if (smallest != i)
            {
                var temp = array[i];
                array[i] = array[smallest];
                array[smallest] = temp;
                MinHeapify(smallest);
            }
        }

        public int Parent(int i)
        {
            return i / 2;
        }
        
        public int Left(int i)
        {
            return 2 * i;
        }

        public int Right(int i)
        {
            return 2 * i + 1;
        }

        public T getMin()
        {
            return array[0];
        }

        public T[] HeapSort()
        {
            for (int i = length-1; i >= 0; i--)
            {
                var temp = array[0];
                array[0] = array[i];
                array[i] = temp;
                heapSize -= 1;
                MinHeapify(0);
            }
            heapSize = length;
            return array;
        }

        public static T[] HeapSort(T[] input)
        {
            var minHeap = new MinHeap<T>(input);
            return minHeap.HeapSort();
        }
    }
}
