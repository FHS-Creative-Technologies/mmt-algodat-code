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
    public static class BreadthFirstSearch<TNode> where TNode : IComparable<TNode>
    {
        public delegate void ProcessNode(TNode node);

        public static void Travers(Graph<TNode> graph, TNode startNode, ProcessNode nodeFunction)
        {
            HashTable<TNode> visitedNodes = new();
            DataStructures.Queue<TNode> nextNodes = new();

            visitedNodes.Add(startNode);
            nextNodes.Enqueue(startNode);
            while (nextNodes.Count > 0)
            {
                TNode currentNode = nextNodes.Dequeue()!; // queue is not empty

                nodeFunction(currentNode);

                foreach (var neighbour in graph.GetEdges(currentNode)!) // currentNode is in graph
                {
                    if (!visitedNodes.Contains(neighbour))
                    {
                        visitedNodes.Add(neighbour);
                        nextNodes.Enqueue(neighbour);
                    }
                }
            }
        }
    }
}
