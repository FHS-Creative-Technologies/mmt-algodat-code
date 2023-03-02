using System;
using System.Collections.Generic;

namespace AlgoDat
{
    public static class MSTKruskal<T> where T : IComparable<T>
    {
        public static WeightedGraph<T> CreateMinimumSpanningTree(WeightedGraph<T> graph)
        {
            UnionFind<T> sets = new();
            foreach (var node in graph)
            {
                sets.MakeSet(node);
            }

            List<WeightedGraph<T>.Edge> edges = new();
            foreach (var node in graph)
            {
                foreach (var edge in graph.GetEdges(node))
                {
                    edges.Add(edge);
                }
            }
            WeightedGraph<T>.Edge[] edgesArray = edges.ToArray();
            MergeSort<WeightedGraph<T>.Edge>.Sort(edgesArray);

            WeightedGraph<T> mst = new();
            foreach (var edge in edgesArray)
            {
                var from = sets.Find(edge.From);
                if (from is not null && from.CompareTo(sets.Find(edge.To)) != 0) 
                {
                    mst.AddUndirectedEdge(edge.From, edge.To, edge.Weight);
                    sets.Union(edge.From, edge.To);
                }
            }

            return mst;
        }
    }
}