// AlgoDat Implementation
// Copyright (C) 2024  Fachhochschule Salzburg / Department Creative Technologies / Andreas Bilke

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

namespace FHS.CT.AlgoDat.Algorithms
{    
    public class SelectionSort<T> where T : IComparable<T>
    {
        public static void Sort(T[] list)
        {
            for (var i = 0; i < list.Length; i++)
            {
                var minPos = i;
                for (var j = i; j < list.Length; j++)
                {
                    if (list[j].CompareTo(list[minPos]) < 0)
                    {
                        minPos = j;
                    }
                }
                T tmp = list[i];
                list[i] = list[minPos];
                list[minPos] = tmp;
            }
        }
    }
}