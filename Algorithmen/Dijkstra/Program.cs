using System;
using System.Collections.Generic;

using AlgoDat;

WeightedGraph<string> graph = new();

graph.AddEdge("s", "t", 10);
graph.AddEdge("s", "y", 5);
graph.AddEdge("t", "x", 1);
graph.AddEdge("t", "y", 2);
graph.AddEdge("y", "t", 3);
graph.AddEdge("y", "x", 9);
graph.AddEdge("y", "z", 2);
graph.AddEdge("x", "z", 4);
graph.AddEdge("z", "x", 6);
graph.AddEdge("z", "s", 7);

List<string> shortestPath = ShortestPath<string>.Compute(graph, "s", "x");

foreach (var node in shortestPath)
{
    Console.WriteLine($"Node {node}");
}