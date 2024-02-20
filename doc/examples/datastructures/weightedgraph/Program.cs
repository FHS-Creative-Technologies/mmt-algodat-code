using FHS.CT.AlgoDat.Datastructures;

WeightedGraph<int> graph = new();

graph.AddNode(5);
graph.AddNode(2);

graph.AddEdge(5, 2, 2.5);
graph.AddEdge(4, 3);
graph.AddUndirectedEdge(7, 1, 3.1415);

foreach (int nodeId in graph)
{
    Console.WriteLine($"Found node {nodeId}");

    foreach (var edge in graph.GetEdges(nodeId))
    {
        Console.WriteLine($"Found edge from {edge.From} to {edge.To} with weight {edge.Weight}");
    }
}