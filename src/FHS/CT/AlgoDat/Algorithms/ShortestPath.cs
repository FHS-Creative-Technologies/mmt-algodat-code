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

using FHS.CT.AlgoDat.DataStructures;

namespace FHS.CT.AlgoDat.Algorithms
{
    public static class ShortestPath<T> where T : IComparable<T>
    {
        public static List<T> Compute(WeightedGraph<T> graph, T from, T to)
        {
            DataStructures.Dictionary<T, double> distance = new();
            DataStructures.Dictionary<T, T> predecessors = new();
            Init(graph, from, distance, predecessors);

            DataStructures.PriorityQueue<T, double> queue = new();
            foreach (var node in graph)
            {
                queue.Enqueue(node, distance.Get(node));
            }

            while (queue.Count > 0)
            {
                var nextNode = queue.Dequeue();

                if (nextNode == null)
                { 
                    break; 
                }

                if (nextNode.Equals(to))
                {
                    break;
                }

                foreach (var edge in graph.GetEdges(nextNode)!) // nextShould is in graph
                {
                    bool newPath = Relax(edge.From, edge.To, edge.Weight, distance, predecessors);
                    if (newPath)
                    {
                        queue.DecreasePriorityValue(edge.To, distance.Get(edge.To));
                    }
                }
            }

            return ConstructPath(from, to, predecessors);
        }

        private static List<T> ConstructPath(T from, T to, DataStructures.Dictionary<T, T> predecessors)
        {
            List<T> path = new();

            T currentNode = to;
            path.Add(currentNode);
            while (!currentNode.Equals(from))
            {
                currentNode = predecessors.Get(currentNode)!; // currentNode is in dict
                path.Add(currentNode);
            }

            path.Reverse();
            return path;
        }

        private static void Init(WeightedGraph<T> graph, T start,
                                 DataStructures.Dictionary<T, double> distance,
                                 DataStructures.Dictionary<T, T> predecessors)
        {
            foreach (var node in graph)
            {
                distance.Add(node, double.MaxValue);
                predecessors.Add(node, default(T));
            }

            distance.Set(start, 0);
        }

        private static bool Relax(T u, T v,
                                  double newDistance, DataStructures.Dictionary<T, double> distance,
                                  DataStructures.Dictionary<T, T> predecessors)
        {
            if (distance.Get(v).CompareTo(distance.Get(u) + newDistance) > 0)
            {
                distance.Set(v, distance.Get(u) + newDistance);
                predecessors.Set(v, u);

                return true;
            }

            return false;
        }
    }
}
