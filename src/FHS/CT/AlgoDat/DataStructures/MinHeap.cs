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
    public class MinHeap<T> where T : IComparable<T>
    {
        private List<T> _heap;

        public int Count
        {
            get
            {
                return _heap.Count;
            }
        }

        public MinHeap()
        {
            _heap = new();
        }

        private int Left(int i)
        {
            return 2*i + 1;
        }

        private int Right(int i)
        {
            return 2*i + 2;
        }

        private int Parent(int i)
        {
            return (i - 1) / 2;
        }

        private void MinHeapify(int i)
        {
            int left = Left(i);
            int right = Right(i);

            int smallest = i;
            if (left < _heap.Count && _heap[left].CompareTo(_heap[i]) < 0)
            {
                smallest = left;
            }

            if (right < _heap.Count && _heap[right].CompareTo(_heap[smallest]) < 0)
            {
                smallest = right;
            }

            if (smallest != i)
            {
                T tmp = _heap[smallest];
                _heap[smallest] = _heap[i];
                _heap[i] = tmp;

                MinHeapify(smallest);
            }
        }

        public T? ExtractMin()
        {
            if (_heap.Count == 0)
            {
                return default(T);
            }

            T min = _heap[0];
            _heap[0] = _heap[_heap.Count - 1];
            _heap.RemoveAt(_heap.Count - 1);

            MinHeapify(0);

            return min;
        }

        public void Insert(T key)
        {
            _heap.Add(key);
            InternalDecreaseKey(_heap.Count - 1, key);
        }

        public void DecreaseKey(int i, T newKey)
        {
            if (!(i >= 0 && i < _heap.Count))
            {
                throw new HeapException("Invalid Index");
            }

            // old key is smaller than new one!
            if (_heap[i].CompareTo(newKey) < 0)
            {
                throw new HeapException("Cannot increase key!");
            }

            InternalDecreaseKey(i, newKey);
        }

        private void InternalDecreaseKey(int i, T newKey)
        {
            _heap[i] = newKey;
            while (i > 0 && _heap[Parent(i)].CompareTo(_heap[i]) > 0)
            {
                T tmp = _heap[Parent(i)];
                _heap[Parent(i)] = _heap[i];
                _heap[i] = tmp;
                i = Parent(i);
            }
        }
     
        public class HeapException : Exception
        {
            public HeapException(string message) : base(message)
            {

            }
        }
    }
}
