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
    public class QuickSort<T> where T : IComparable<T>
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