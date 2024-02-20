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

using System.Collections;

namespace FHS.CT.AlgoDat.Datastructures
{
    public class Dictionary<TKey, TValue> : IEnumerable<Dictionary<TKey, TValue>.KeyValuePair> where TKey : IComparable<TKey>
    {
        public class KeyValuePair : IComparable<KeyValuePair>
        {
            public TKey Key { get; set; }
            public TValue? Value { get; set; }

            public KeyValuePair(TKey key)
            {
                Key = key;
            }

            public KeyValuePair(TKey key, TValue? value)
            {
                Key = key;
                Value = value;
            }

            public int CompareTo(KeyValuePair? other)
            {
                return Key.CompareTo(other!.Key);
            }

            public override int GetHashCode()
            {
                return Key.GetHashCode();
            }
        }

        public class DictionaryEnumerator : IEnumerator<KeyValuePair>
        {
            private IEnumerator<KeyValuePair> _hashTableEnumerator;
            public KeyValuePair Current
            {
                get
                {
                    return _hashTableEnumerator.Current;
                }
            }

            object IEnumerator.Current 
            {
                get
                {
                    return Current;
                }
            }

            public DictionaryEnumerator(HashTable<KeyValuePair> hashTable)
            {
                _hashTableEnumerator = hashTable.GetEnumerator();
            }

            public void Dispose()
            {
                
            }

            public bool MoveNext()
            {
                return _hashTableEnumerator.MoveNext();
            }

            public void Reset()
            {
                _hashTableEnumerator.Reset();
            }
        }

        private HashTable<KeyValuePair> _hashTable;

        public Dictionary(int initialCapacity = 256)
        {
            _hashTable = new(initialCapacity);
        }

        public void Add(TKey key, TValue? value)
        {
            var kvp = new KeyValuePair(key, value);
            if (_hashTable.Search(kvp) == null)
            {
                _hashTable.Add(kvp);
            }
        }

        public TValue Get(TKey key)
        {
            var result = _hashTable.Search(new KeyValuePair(key));

            if (result == null)
            {
                return default(TValue)!;
            }

            return result.Value!;
        }

        public void Set(TKey key, TValue newValue)
        {
            var result = _hashTable.Search(new KeyValuePair(key));

            if (result == null)
            {
                throw new Exception($"Key {key} does not exist in dictionary");
            }

            result.Value = newValue;
        }

        public void Remove(TKey key)
        {
            _hashTable.Delete(new KeyValuePair(key));
        }

        public bool Contains(TKey key)
        {
            return _hashTable.Search(new KeyValuePair(key)) != null ? true : false;
        }

        public IEnumerator<KeyValuePair> GetEnumerator()
        {
            return new DictionaryEnumerator(_hashTable);
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }
    }
}