using System;

namespace AlgoDat
{    
    class InsertionSort<T> where T : IComparable<T>
    {
        public static void Sort(T[] list)
        {
            for (int j = 1; j < list.Length; j++)
            {
                T key = list[j];

                int i = j - 1;
                while (i >= 0 && list[i].CompareTo(key) > 0)
                {
                    list[i + 1] = list[i];
                    i--;
                }

                list[i + 1] = key;
            }
        }
    }
}