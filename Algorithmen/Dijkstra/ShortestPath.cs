using System;
using System.Collections.Generic;

namespace AlgoDat
{
    public static class ShortestPath<T> where T : IComparable<T>
    {
        public static List<T> Compute(WeightedGraph<T> graph, T from, T to)
        {
            Dictionary<T, double> distance = new();
            Dictionary<T, T> predecessors = new();
            Init(graph, from, distance, predecessors);

            PriorityQueue<T, double> queue = new();
            foreach (var node in graph)
            {
                queue.Enqueue(node, distance.Get(node));
            }

            while (queue.Count > 0)
            {
                var nextNode = queue.Dequeue();

                if (nextNode is null)
                { 
                    break; 
                }

                if (nextNode.Equals(to))
                {
                    break;
                }

                foreach (var edge in graph.GetEdges(nextNode))
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

        private static List<T> ConstructPath(T from, T to, Dictionary<T, T> predecessors)
        {
            List<T> path = new();

            T currentNode = to;
            path.Add(currentNode);
            while (!currentNode.Equals(from))
            {
                currentNode = predecessors.Get(currentNode);
                path.Add(currentNode);
            }

            path.Reverse();
            return path;
        }

        private static void Init(WeightedGraph<T> graph, T start,
                                 Dictionary<T, double> distance,
                                 Dictionary<T, T> predecessors)
        {
            foreach (var node in graph)
            {
                distance.Add(node, double.MaxValue);
                predecessors.Add(node, default(T));
            }

            distance.Set(start, 0);
        }

        private static bool Relax(T u, T v,
                                  double newDistance, Dictionary<T, double> distance,
                                  Dictionary<T, T> predecessors)
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