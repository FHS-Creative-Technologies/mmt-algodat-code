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

using FHS.CT.AlgoDat.Datastructures;

namespace FHS.CT.AlgoDat.Algorithms
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