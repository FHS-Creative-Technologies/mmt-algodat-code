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
    public static class DepthFirstSearch<TNode> where TNode : IComparable<TNode>
    {
        public delegate void ProcessNode(TNode node);

        public static void TraversIterative(Graph<TNode> graph, TNode startNode, ProcessNode nodeFunction)
        {
            HashTable<TNode> visitedNodes = new();
            DataStructures.Stack<TNode> nextNodes = new();

            visitedNodes.Add(startNode);
            nextNodes.Push(startNode);
            while (nextNodes.Count > 0)
            {
                var currentNode = nextNodes.Pop()!;

                nodeFunction(currentNode);
                foreach (var neighbour in graph.GetEdges(currentNode)!)
                {
                    if (!visitedNodes.Contains(neighbour))
                    {
                        visitedNodes.Add(neighbour);
                        nextNodes.Push(neighbour);
                    }
                }
            }
        }

        public static void Travers(Graph<TNode> graph, TNode startNode, ProcessNode nodeFunction)
        {
            DataStructures.Dictionary<TNode, Color> nodeColors = new();

            foreach (var node in graph)
            {
                nodeColors.Add(node, Color.WHITE);
            }

            Travers(graph, startNode, nodeFunction, nodeColors);
        }

        private static void Travers(Graph<TNode> graph, TNode currentNode, ProcessNode nodeFunction, DataStructures.Dictionary<TNode, Color> nodeColors)
        {
            nodeColors.Set(currentNode, Color.GRAY);
            nodeFunction(currentNode);

            foreach (var neighbour in graph.GetEdges(currentNode))
            {
                if (nodeColors.Get(neighbour).Equals(Color.WHITE))
                {
                    Travers(graph, neighbour, nodeFunction, nodeColors);
                }
            }

            nodeColors.Set(currentNode, Color.BLACK);
        }

        public enum Color
        {
            WHITE,
            GRAY,
            BLACK
        }
    }


}
