using System.Collections;

namespace AlgoDat
{
    public class UnionFind<T> : IEnumerable<UnionFind<T>.Set> where T : IComparable<T>
    {
        public class UnionFindException : Exception
        {
            public UnionFindException(string message) : base(message) { }
        }

        public class UnionFindEnumerator : IEnumerator<Set>
        {
            private HashTable<Set> _sets;

            private IEnumerator<Set> _setsEnumerator;

            public Set Current
            {
                get
                {
                    return _setsEnumerator.Current;
                }
            }

            object IEnumerator.Current
            {
                get
                {
                    return Current;
                }
            }

            public UnionFindEnumerator(Dictionary<T, Set.Node> elementsToNode)
            {
                _sets = new();

                // this is super inefficient. 
                //
                // *If you can propose a better solution
                //  you will get a cookie reward from the
                //  lecturer of the algorithm and data structure
                //  class*
                //
                // Also: Please ask the author of this code
                // to attend an algorithm and
                // data structure class.
                foreach (var kvp in elementsToNode)
                {
                    if (kvp.Value is not null)
                    {
                        // if the set of the current element is
                        // not in the list, add it.
                        if (_sets.Search(kvp.Value.Parent) == null)
                        {
                            _sets.Add(kvp.Value.Parent);
                        }
                    }
                }

                _setsEnumerator = _sets.GetEnumerator();
            }

            public void Dispose()
            {

            }

            public bool MoveNext()
            {
                return _setsEnumerator.MoveNext();
            }

            public void Reset()
            {
                _setsEnumerator.Reset();
            }
        }

        public class Set : IEnumerable<Set.Node>, IComparable<Set>
        {
            public class Node
            {
                public T Data { get; }
                public Set Parent { get; set; }
                public Node? Next { get; set; }

                public Node(T data, Set set)
                {
                    Data = data;
                    Parent = set;
                }
            }

            public T Ambassador
            {
                get
                {
                    return Head.Data;
                }
            }

            public Node Head { get; }

            private Node Tail { get; set; }

            public int Count { get; private set; }

            public Set(T initalNodeData)
            {
                Node ambassadorNode = new(initalNodeData, this);

                Head = ambassadorNode;
                Tail = ambassadorNode;

                Count = 1;
            }

            public Node Append(T newNodeData)
            {
                Node newNode = new(newNodeData, this);

                Tail.Next = newNode;
                Tail = newNode;

                Count++;

                return newNode;
            }

            public IEnumerator<Node> GetEnumerator()
            {
                var node = Head;
                while (node != null)
                {
                    yield return node;
                    node = node.Next;
                }
            }

            IEnumerator IEnumerable.GetEnumerator()
            {
                return GetEnumerator();
            }

            public int CompareTo(Set? other)
            {
                if (other is null || Ambassador is null)
                { 
                    return int.MaxValue; 
                }
                else
                    return Ambassador.CompareTo(other.Ambassador);
            }
        }

        /// <summary>This one is needed for finding (fast) the
        /// set for one element</summary>
        private Dictionary<T, Set.Node> _elementToNode;

        public UnionFind()
        {
            _elementToNode = new();
        }

        public void MakeSet(T element)
        {
            Set elementSet = new(element);
            _elementToNode.Add(element, elementSet.Head);
        }

        public T? Find(T element)
        {
            if (!_elementToNode.Contains(element))
            {
                throw new UnionFindException($"{element} does not belong to any set");
            }

            return _elementToNode.Get(element).Parent.Ambassador;
        }

        /// <summary>Join two disjoint sets.</summary>
        /// <param name="element1">Member of first set</param>
        /// <param name="element2">Member of second set</param>
        public void Union(T element1, T element2)
        {
            if (!_elementToNode.Contains(element1) || !_elementToNode.Contains(element2))
            {
                throw new UnionFindException($"{element1} or {element2} does not belong to any set");
            }

            var set1 = _elementToNode.Get(element1).Parent;
            var set2 = _elementToNode.Get(element2).Parent;

            if (set1.Equals(set2))
            {
                throw new UnionFindException($"Cannot union Set. {element1} or {element2} belong to the same set");
            }

            // swap sets such that we append the shorter set to the longer one
            if (set2.Count > set1.Count)
            {
                var tmp = set1;
                set1 = set2;
                set2 = tmp;
            }

            foreach (var node in set2)
            {
                var newNode = set1.Append(node.Data);
                _elementToNode.Set(node.Data, newNode);
            }
        }

        public IEnumerator<Set> GetEnumerator()
        {
            return new UnionFindEnumerator(_elementToNode);
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }
    }
}