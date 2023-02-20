using AlgoDat;
using System;

Graph<int> graph = new();

graph.AddNode(5);
graph.AddNode(2);

graph.AddEdge(5, 2);

graph.AddEdge(4, 3);

foreach (int nodeId in graph)
{
    Console.WriteLine($"Node {nodeId}");

    foreach (int edge in graph.GetEdges(nodeId))
    {
        Console.WriteLine($"\tHas edge to {edge}");
    }
}