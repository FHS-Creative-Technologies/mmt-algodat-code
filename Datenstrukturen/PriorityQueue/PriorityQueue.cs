using System;
using System.Collections.Generic;

namespace AlgoDat
{
    /// <summmary>
    /// Implement a priority queue with the logic of a min-heap.
    /// This implementation also implements an efficient way of decreasing
    /// the priority value of one element
    /// </summary>
    public class PriorityQueue<TElement, TPriority> where TPriority : IComparable<TPriority> where TElement : IComparable<TElement>
    {
        class HeapKeyValuePair : IComparable<HeapKeyValuePair>
        {
            public TPriority Priority { get; set; }

            public TElement Element { get; set; }

            public HeapKeyValuePair(TPriority priority, TElement element)
            {
                Priority = priority;
                Element = element;
            }

            public int CompareTo(HeapKeyValuePair other)
            {
                return Priority.CompareTo(other.Priority);
            }
        }

        private List<HeapKeyValuePair> _heap;
        private Dictionary<TElement, int> _indexMap;

        public int Count
        {
            get
            {
                return _heap.Count;
            }
        }

        public PriorityQueue()
        {
            _heap = new();
            _indexMap = new();
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
                var tmp = _heap[smallest];
                _heap[smallest] = _heap[i];
                _heap[i] = tmp;

                var elemSmallest = _heap[smallest].Element;
                var elemLarger = _heap[i].Element;

                _indexMap.Set(elemSmallest, smallest);
                _indexMap.Set(elemLarger, i);

                MinHeapify(smallest);
            }
        }

        public TElement Dequeue()
        {
            if (_heap.Count == 0)
            {
                return default(TElement);
            }

            TElement min = _heap[0].Element;
            _heap[0] = _heap[_heap.Count - 1];
            _indexMap.Set(_heap[0].Element, 0);

            _heap.RemoveAt(_heap.Count - 1);
            _indexMap.Remove(min);

            MinHeapify(0);

            return min;
        }

        public void Enqueue(TElement element, TPriority priority)
        {
            HeapKeyValuePair kvp = new(priority, element);

            _heap.Add(kvp);
            _indexMap.Add(element, _heap.Count - 1);

            InternalDecreaseKey(_heap.Count - 1, kvp);
        }

        public void DecreasePriorityValue(TElement element, TPriority newPriority)
        {
            int indexOfElement = _indexMap.Get(element);

            HeapKeyValuePair newKvp = new(newPriority, element);

            // old key is smaller than new one!
            if (_heap[indexOfElement].CompareTo(newKvp) < 0)
            {
                throw new HeapException("Cannot increase key!");
            }

            InternalDecreaseKey(indexOfElement, newKvp);
        }

        public bool Contains(TElement element)
        {
            return _indexMap.Contains(element);
        }

        private void InternalDecreaseKey(int i, HeapKeyValuePair newKvp)
        {
            _heap[i] = newKvp;
            _indexMap.Set(newKvp.Element, i);

            while (i > 0 && _heap[Parent(i)].CompareTo(_heap[i]) > 0)
            {
                var tmp = _heap[Parent(i)];
                _heap[Parent(i)] = _heap[i];
                _heap[i] = tmp;

                _indexMap.Set(_heap[Parent(i)].Element, Parent(i));
                _indexMap.Set(_heap[i].Element, i);

                i = Parent(i);
            }
        }
    }

    public class HeapException : Exception
    {
        public HeapException(string message) : base(message)
        {

        }
    }
}