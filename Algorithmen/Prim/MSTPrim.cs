using System;

namespace AlgoDat
{
    public static class MSTPrim<T> where T : IComparable<T>
    {
        public static WeightedGraph<T> CreateMinimumSpanningTree(WeightedGraph<T> graph, T startNode)
        {
            Dictionary<T, double> weights = new();
            Dictionary<T, T> predecessors = new();

            foreach (var node in graph)
            {
                weights.Add(node, double.MaxValue);
                predecessors.Add(node, default(T));
            }
            weights.Set(startNode, 0);

            PriorityQueue<T, double> queue = new();
            foreach (var node in graph)
            {
                queue.Enqueue(node, weights.Get(node));
            }

            while (queue.Count > 0)
            {
                var minNode = queue.Dequeue();
                foreach (var edge in graph.GetEdges(minNode))
                {
                    if (queue.Contains(edge.To) && edge.Weight.CompareTo(weights.Get(edge.To)) < 0)
                    {
                        predecessors.Set(edge.To, minNode);
                        weights.Set(edge.To, edge.Weight);

                        queue.DecreasePriorityValue(edge.To, edge.Weight);
                    }
                }
            }

            return GraphFromPredecessorList(predecessors, weights, startNode);
        }

        private static WeightedGraph<T> GraphFromPredecessorList(Dictionary<T, T> predecessors, Dictionary<T, double> weights, T startNode)
        {
            WeightedGraph<T> mst = new();

            foreach (var kvp in predecessors)
            {
                if (!kvp.Key.Equals(startNode))
                {
                    mst.AddUndirectedEdge(kvp.Key, kvp.Value, weights.Get(kvp.Key));
                }
            }

            return mst;
        }
    }
}