using System;
using System.Collections.Generic;
using System.Reflection;
using System.Text;

namespace HeapSort
{
    public abstract class Tree
    {
        int heapLength;
        public Tree()
        {
        }
        abstract public int GetLength();
        abstract public DatePrice GetByIndex(int index);
        abstract public void PrintToFile(string fileDir);
        abstract public void FillFromFile(string fileDir);
        public abstract void Swap(int i, int j);

        public void HeapSort()
        {
            BuildMaxHeap();
            for (int i = heapLength ; i >= 1; i--)
            {
                Swap(1, i);
                heapLength--;
                Heapify(1);
            }
        }

        public void BuildMaxHeap()
        {
            heapLength = GetLength();
            for (int i = (heapLength) / 2; i >= 1; i--) 
            {
                Heapify(i);
            }
        }
        void Heapify(int i)
        {
            int left = 2 * i;
            int right = 2 * i + 1;
            int max;
            if (left <= heapLength && GetByIndex(left).CompareTo(GetByIndex(i)) > 0)
            {
                max = left;
            }
            else
            {
                max = i;
            }

            if (right <= heapLength && GetByIndex(right).CompareTo(GetByIndex(max)) > 0)
            {
                max = right;
            }
            if (max != i)
            {
                Swap(i, max);
                Heapify(max);
            }
        }
    }
}
