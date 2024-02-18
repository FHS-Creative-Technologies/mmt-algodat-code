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

namespace FHS.CT.AlgoDat
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
                
            }

            public bool MoveNext()
            {
                return _enumerator.MoveNext();
            }

            public void Reset()
            {
                _enumerator.Reset();
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