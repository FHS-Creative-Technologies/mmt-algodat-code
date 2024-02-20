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

namespace FHS.CT.AlgoDat.DataStructures
{
    public class Stack<T> where T : IComparable<T>
    {
        private DoubleLinkedList<T> _list;

        public int Count
        {
            get
            {
                return _list.Count;
            }
        }

        public Stack()
        {
            _list = new();
        }

        public void Push(T elem)
        {
            _list.Append(elem);
        }

        public T? Pop()
        {
            DoubleLinkedList<T>.Node<T>? elem = _list.Tail;

            if(elem is null)
            {
                return default(T);
            }

            _list.Delete(elem);

            return elem.Key;
        }
    }
}
