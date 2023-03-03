namespace AlgoDat
{
    public class MergeSort<T> where T : IComparable<T>
    {
        public static void Sort(T[] list)
        {
            Sort(list, 0, list.Length - 1);
        }

        private static void Sort(T[] list, int left, int right)
        {
            if (left < right)
            {
                int middle = (left + right) / 2;
                Sort(list, left, middle);
                Sort(list, middle + 1, right);
                Merge(list, left, middle, right);
            }
        }

        private static void Merge(T[] list, int left, int middle, int right)
        {
            int lPointer = left;
            int rPointer = middle + 1;
            T[] temp = new T[right - left + 1];
            int cPointer = 0;

            while (lPointer <= middle && rPointer <= right)
            {
                if (list[lPointer].CompareTo(list[rPointer]) <= 0)
                {
                    temp[cPointer] = list[lPointer];
                    lPointer++;
                }
                else
                {
                    temp[cPointer] = list[rPointer];
                    rPointer++;
                }
                cPointer++;
            }

            while (lPointer <= middle)
            {
                temp[cPointer] = list[lPointer];
                lPointer++;
                cPointer++;
            }

            while (rPointer <= right)
            {
                temp[cPointer] = list[rPointer];
                rPointer++;
                cPointer++;
            }

            for (int i = 0; i < temp.Length; i++)
            {
                list[left + i] = temp[i];
            }
        }
    }
}