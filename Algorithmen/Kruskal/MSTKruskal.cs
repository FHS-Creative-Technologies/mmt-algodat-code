namespace AlgoDat
{
    public static class MSTKruskal<T> where T : IComparable<T>
    {
        public class EdgeWeightComparer : IComparer<WeightedGraph<T>.Edge>
        {
            public int Compare(WeightedGraph<T>.Edge? x, WeightedGraph<T>.Edge? y)
            {
                return x!.Weight.CompareTo(y!.Weight);
            }
        }

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
            Array.Sort<WeightedGraph<T>.Edge>(edgesArray, new EdgeWeightComparer());

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