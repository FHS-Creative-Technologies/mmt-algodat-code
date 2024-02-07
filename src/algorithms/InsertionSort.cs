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
    public class InsertionSort<T> where T : IComparable<T>
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