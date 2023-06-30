using System.Collections;

namespace AlgoDat
{
    public class HashTable<T> : IEnumerable<T> where T : IComparable<T>
    {
        public class HashTableEnumerator : IEnumerator<T>
        {
            private DoubleLinkedList<T>[]? _bins;

            private int _currentBin;

            private IEnumerator<T> _currentBinEnumerator;

            public T Current
            {
                get
                {
                    return _currentBinEnumerator.Current;
                }
            }

            object IEnumerator.Current
            {
                get
                {
                    return Current;
                }
            }

            public HashTableEnumerator(DoubleLinkedList<T>[] bins)
            {
                _bins = bins;
                _currentBin = 0;
                _currentBinEnumerator = _bins[_currentBin].GetEnumerator();
            }

            public void Dispose()
            {
                
            }

            public bool MoveNext()
            {
                if (_currentBinEnumerator.MoveNext())
                {
                    return true;
                }

                if (!FindNextBinEnumerator())
                {
                    return false;
                }

                _currentBinEnumerator.MoveNext();

                return true;
            }

            private bool FindNextBinEnumerator()
            {
                // dispose old bin enumerator and find new one
                bool foundNew = false;
                _currentBin++;

                while (_bins is not null && _currentBin < _bins.Length && !foundNew)
                {
                    if (_bins[_currentBin].Count > 0)
                    {
                        _currentBinEnumerator = _bins[_currentBin].GetEnumerator();
                        foundNew = true;
                        return true;
                    }
                 
                    _currentBin++;
                }

                return false;
            }

            public void Reset()
            {
                _bins = null;
            }
        }

        public int Count { get; private set; }

        private double Load { get => Count / (double)_bins.Length; }

        private DoubleLinkedList<T>[] _bins;

        private const double LoadThreshold = 1.5;

        private const int ResizeFactor = 2;

        public HashTable(int initialCapacity = 256)
        {
            _bins = new DoubleLinkedList<T>[initialCapacity];
            InitBins(initialCapacity);
        }

        private void InitBins(int capacity)
        {
            _bins = new DoubleLinkedList<T>[capacity];

            for (int i = 0; i < _bins.Length; i++)
            {
                _bins[i] = new DoubleLinkedList<T>();
            }
        }

        private int GetElementHash(T elem)
        {
            int hashCode = elem.GetHashCode() % _bins.Length;

            return hashCode >= 0 ? hashCode : -hashCode;
        }

        /// <summary>Increase size of hash table and rehash all existing entries into the new table</summary>
        private void ReorganizeTable()
        {
            var oldBins = _bins;

            InitBins(_bins.Length * ResizeFactor);

            for (int i = 0; i < oldBins.Length; i++)
            {
                var listPointer = oldBins[i].Head;
                while (listPointer != null)
                {
                    int hashCode = GetElementHash(listPointer.Key);
                    _bins[hashCode].Append(listPointer.Key);

                    listPointer = listPointer.Next;
                }
            }
        }

        public void Add(T value)
        {
            if (Load >= LoadThreshold)
            {
                ReorganizeTable();
            }

            int hashCode = GetElementHash(value);
            _bins[hashCode].Append(value);
            Count++;
        }

        public void Delete(T value)
        {
            int hashCode = GetElementHash(value);
            var kvpNode = _bins[hashCode].Search(value);
            if (kvpNode == null)
            {
                return; // nothing to do here
            }

            _bins[hashCode].Delete(kvpNode);
            Count--;
        }

        public T? Search(T value)
        {
            int hashCode = GetElementHash(value);
            var kvpNode = _bins[hashCode].Search(value);

            return kvpNode == null ? default(T) : kvpNode.Key;
        }

        public bool Contains(T value)
        {
            return !object.Equals(Search(value), default(T));
        }

        public IEnumerator<T> GetEnumerator()
        {
            return new HashTableEnumerator(_bins);
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }
    }
}
