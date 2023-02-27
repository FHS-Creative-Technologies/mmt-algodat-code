using System;
using System.Collections;
using System.Collections.Generic;

namespace AlgoDat
{
    public class WeightedGraph<TNode> : IEnumerable<TNode> where TNode : IComparable<TNode>
    {
        public class WeightedGraphEnumerator : IEnumerator<TNode>
        {
            private IEnumerator<Dictionary<TNode, DoubleLinkedList<Edge>>.KeyValuePair> _enumerator;

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

            public WeightedGraphEnumerator(Dictionary<TNode, DoubleLinkedList<Edge>> nodes)
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

        public class Edge : IComparable<Edge>
        {
            public TNode From { get; }
            public TNode To { get; }
            public double Weight { get; }

            public Edge(TNode from, TNode to, double weight)
            {
                From = from;
                To = to;
                Weight = weight;
            }

            public int CompareTo(Edge other)
            {
                if (   From.CompareTo(other.From)     == 0
                    && To.CompareTo(other.To)         == 0
                    && Weight.CompareTo(other.Weight) == 0)
                {
                    return 0;
                }

                return -1; // no order there because we can't tell properly
            }
        }

        private Dictionary<TNode, DoubleLinkedList<Edge>> _nodes;

        public WeightedGraph()
        {
            _nodes = new();
        }

        public void AddNode(TNode node)
        {
            if (!_nodes.Contains(node))
            {
                _nodes.Add(node, new DoubleLinkedList<Edge>());
            }
        }

        public void AddEdge(TNode from, TNode to, double weight = 1.0)
        {
            if (!_nodes.Contains(from))
            {
                AddNode(from);
            }

            if (!_nodes.Contains(to))
            {
                AddNode(to);
            }

            Edge newEdge = new Edge(from, to, weight);
            var edges = _nodes.Get(from);
            if (edges.Search(newEdge) == null)
            {
                edges.Append(newEdge);
            }
        }

        public void AddUndirectedEdge(TNode n1, TNode n2, double weight = 1.0)
        {
            AddEdge(n1, n2, weight);
            AddEdge(n2, n1, weight);
        }

        public DoubleLinkedList<Edge> GetEdges(TNode node)
        {
            return _nodes.Get(node);
        }

        public IEnumerator<TNode> GetEnumerator()
        {
            return new WeightedGraphEnumerator(_nodes);
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }

        public string ToDotFormat(bool asUndirected = false)
        {
            string output = asUndirected ? "graph {\n" : "digraph {\n";

            foreach (var nodeKvp in _nodes)
            {
                foreach (var edge in nodeKvp.Value)
                {
                    if (asUndirected)
                    {
                        // only print the "smaller" node connection
                        if (edge.From.CompareTo(edge.To) < 0)
                        {
                            output += $"{edge.From} -- {edge.To} [label=\"{edge.Weight}\"];\n";
                        }
                    }
                    else
                    {
                        output += $"{edge.From} -> {edge.To} [label=\"{edge.Weight}\"];\n";
                    }
                    
                }
            }

            output += "}";

            return output;
        }
    }
}
