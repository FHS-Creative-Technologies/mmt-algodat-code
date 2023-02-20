using System;

namespace AlgoDat
{
        class QuickSort<T> where T : IComparable<T>
    {
        public static void Sort(T[] list)        
        {
            Sort(list, 0, list.Length - 1);
        }

        private static void Sort(T[] list, int left, int right)
        {
            if (left < right)
            {
                int pivot = Partition(list, left, right);
                Sort(list, left, pivot - 1);
                Sort(list, pivot + 1, right);
            }
        }

        private static int Partition(T[] list, int left, int right)
        {
            T x = list[right];
            int pivot = left - 1;

            for (int i = left; i < right; i++)
            {
                if (list[i].CompareTo(x) <= 0)
                {
                    pivot++;
                    T tmp = list[i];
                    list[i] = list[pivot];
                    list[pivot] = tmp;
                }
            }

            T tmpPivot = list[pivot + 1];
            list[pivot + 1] = list[right];
            list[right] = tmpPivot;

            return pivot + 1;
        }
    }
}