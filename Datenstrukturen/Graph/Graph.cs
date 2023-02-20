using System;
using System.Collections;
using System.Collections.Generic;

namespace AlgoDat
{
    public class Graph<TNode> : IEnumerable<TNode> where TNode : IComparable<TNode>
    {
        public class GraphEnumerator : IEnumerator<TNode>
        {
            private IEnumerator<Dictionary<TNode, DoubleLinkedList<TNode>>.KeyValuePair> _enumerator;

            public TNode Current
            {
                get
                {
                    return _enumerator.Current.Key;
                }
            }

            object IEnumerator.Current
            {
                get
                {
                    return Current;
                }                
            }

            public GraphEnumerator(Dictionary<TNode, DoubleLinkedList<TNode>> nodes)
            {
                _enumerator = nodes.GetEnumerator();
            }

            public void Dispose()
            {
                _enumerator = null;
            }

            public bool MoveNext()
            {
                return _enumerator.MoveNext();
            }

            public void Reset()
            {
                _enumerator = null;
            }
        }

        private Dictionary<TNode, DoubleLinkedList<TNode>> _nodes;

        public Graph()
        {
            _nodes = new();
        }

        public void AddNode(TNode node)
        {
            if (!_nodes.Contains(node))
            {
                _nodes.Add(node, new DoubleLinkedList<TNode>());
            }
        }

        public void AddEdge(TNode from, TNode to)
        {
            if (!_nodes.Contains(from))
            {
                AddNode(from);
            }

            if (!_nodes.Contains(to))
            {
                AddNode(to);
            }

            var edges = _nodes.Get(from);
            if (edges.Search(to) == null)
            {
                edges.Append(to);
            }
            
        }

        public DoubleLinkedList<TNode> GetEdges(TNode node)
        {
            return _nodes.Get(node);
        }

        public IEnumerator<TNode> GetEnumerator()
        {
            return new GraphEnumerator(_nodes);
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }
    }

    public class GraphException : Exception
    {
        public GraphException(string message) : base(message) {}
    }
}