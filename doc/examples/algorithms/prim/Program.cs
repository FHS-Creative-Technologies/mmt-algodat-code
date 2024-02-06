using FHS.CT.AlgoDat;

WeightedGraph<string> graph = new();

graph.AddUndirectedEdge("a", "b", 4);
graph.AddUndirectedEdge("b", "c", 8);
graph.AddUndirectedEdge("c", "d", 7);
graph.AddUndirectedEdge("d", "e", 9);
graph.AddUndirectedEdge("e", "f", 10);
graph.AddUndirectedEdge("f", "g", 2);
graph.AddUndirectedEdge("g", "h", 1);
graph.AddUndirectedEdge("h", "a", 8);
graph.AddUndirectedEdge("b", "h", 11);
graph.AddUndirectedEdge("h", "i", 7);
graph.AddUndirectedEdge("i", "c", 2);
graph.AddUndirectedEdge("i", "g", 6);
graph.AddUndirectedEdge("c", "f", 4);
graph.AddUndirectedEdge("d", "f", 14);

var mst = MSTPrim<string>.CreateMinimumSpanningTree(graph, "a");
Console.WriteLine(mst.ToDotFormat(true));