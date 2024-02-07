// AlgoDat Implementation
// Copyright (C) 2024  Fachhochschule Salzburg / Department Creative Technologie / Andreas Bilke

// This program is free software: you can redistribute it and/or modify
// it under the terms of the GNU General Public License as published by
// the Free Software Foundation, either version 3 of the License, or
// (at your option) any later version.

// This program is distributed in the hope that it will be useful,
// but WITHOUT ANY WARRANTY; without even the implied warranty of
// MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
// GNU General Public License for more details.

// You should have received a copy of the GNU General Public License
// along with this program.  If not, see <http://www.gnu.org/licenses/>.

namespace FHS.CT.AlgoDat
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