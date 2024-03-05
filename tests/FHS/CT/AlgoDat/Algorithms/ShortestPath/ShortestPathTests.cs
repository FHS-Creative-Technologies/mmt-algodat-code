using FHS.CT.AlgoDat.Algorithms;
using FHS.CT.AlgoDat.DataStructures;

namespace ShortestPath;

public class ShortestPathTests
{
    [Fact]
    public void TestDijkstra()
    {
        var weightedGraph = new WeightedGraph<char>();
        
        weightedGraph.AddEdge('a', 'b', 2);
        weightedGraph.AddEdge('a', 'c', 1);
        weightedGraph.AddEdge('a', 'd', 3);
        
        weightedGraph.AddEdge('b', 'd', 2);
        weightedGraph.AddEdge('b', 'c', 7);
        
        weightedGraph.AddEdge('c', 'f', 3);
        weightedGraph.AddEdge('c', 'e', 3);
        
        weightedGraph.AddEdge('d', 'f', 1);
        weightedGraph.AddEdge('d', 'e', 1);
        weightedGraph.AddEdge('d', 'g', 4);
        
        weightedGraph.AddEdge('e', 'g', 3);
        
        weightedGraph.AddEdge('f', 'g', 2);

        var shortestPath = ShortestPath<char>.Compute(weightedGraph, 'a', 'g');
        var expectedShortestPath = new List<char>
        {
            'a', 'c', 'f', 'g'
        };
        Assert.Equal(expectedShortestPath, shortestPath);
    }
}
