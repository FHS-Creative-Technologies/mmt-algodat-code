using System;
using System.Collections;
using System.Collections.Generic;

namespace AlgoDat
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