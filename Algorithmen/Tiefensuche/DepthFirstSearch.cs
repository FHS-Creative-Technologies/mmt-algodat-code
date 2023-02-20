using System;

namespace AlgoDat
{
    public static class DepthFirstSearch<TNode> where TNode : IComparable<TNode>
    {
        public delegate void ProcessNode(TNode node);

        public static void TraversIterative(Graph<TNode> graph, TNode startNode, ProcessNode nodeFunction)
        {
            HashTable<TNode> visitedNodes = new();
            Stack<TNode> nextNodes = new();

            visitedNodes.Add(startNode);
            nextNodes.Push(startNode);
            while (nextNodes.Count > 0)
            {
                var currentNode = nextNodes.Pop();
                nodeFunction(currentNode);
                
                foreach (var neighbour in graph.GetEdges(currentNode))
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
             Dictionary<TNode, Color> nodeColors = new();

             foreach (var node in graph)
             {
                nodeColors.Add(node, Color.WHITE);
             }

             Travers(graph, startNode, nodeFunction, nodeColors);
         }

         private static void Travers(Graph<TNode> graph, TNode currentNode, ProcessNode nodeFunction, Dictionary<TNode, Color> nodeColors)
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