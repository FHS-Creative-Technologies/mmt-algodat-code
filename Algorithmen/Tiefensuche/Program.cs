using System;

using AlgoDat;

Graph<string> graph = new();

graph.AddEdge("a", "b"); graph.AddEdge("b", "a");
graph.AddEdge("a", "d"); graph.AddEdge("d", "a");
graph.AddEdge("d", "b"); graph.AddEdge("b", "d");
graph.AddEdge("b", "e"); graph.AddEdge("e", "b");
graph.AddEdge("d", "g"); graph.AddEdge("g", "d");
graph.AddEdge("e", "g"); graph.AddEdge("g", "e");
graph.AddEdge("e", "f"); graph.AddEdge("f", "e");
graph.AddEdge("f", "c"); graph.AddEdge("c", "f");

DepthFirstSearch<string>.TraversIterative(graph, "a", DoWithNode);

Console.WriteLine("Recursive");
DepthFirstSearch<string>.Travers(graph, "a", DoWithNodeRecursive);

static void DoWithNode(string node)
{
    Console.WriteLine($"Process node {node}");
}

static void DoWithNodeRecursive(string node)
{
    Console.WriteLine($"Process recursive node {node}");
}