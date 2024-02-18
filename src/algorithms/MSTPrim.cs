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

namespace FHS.CT.AlgoDat
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

                if (minNode is null)
                { 
                    break;
                }

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
                if (!kvp.Key.Equals(startNode) && kvp.Value is not null)
                {
                    mst.AddUndirectedEdge(kvp.Key, kvp.Value, weights.Get(kvp.Key));
                }
            }

            return mst;
        }
    }
}