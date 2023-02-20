using System;

namespace AlgoDat
{
    public static class BreadthFirstSearch<TNode> where TNode : IComparable<TNode>
    {
        public delegate void ProcessNode(TNode node);

        public static void Travers(Graph<TNode> graph, TNode startNode, ProcessNode nodeFunction)
        {
            HashTable<TNode> visitedNodes = new();
            Queue<TNode> nextNodes = new();

            visitedNodes.Add(startNode);
            nextNodes.Enqueue(startNode);
            while (nextNodes.Count > 0)
            {
                var currentNode = nextNodes.Dequeue();
                nodeFunction(currentNode);
                
                foreach (var neighbour in graph.GetEdges(currentNode))
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