using FHS.CT.AlgoDat.DataStructures;

namespace WeightedGraph;

public class WeightedGraphTests
{
    [Fact]
    public void TestWeightedGraph()
    {
        var graph = new WeightedGraph<char>();
        graph.AddEdge('a', 'b', 10);
        graph.AddEdge('a', 'c', 20);
        graph.AddEdge('d', 'b', 30);
        graph.AddEdge('c', 'b', 40);

        var nodes = new List<char>()
        {
            'a', 'b', 'c', 'd'
        };

        foreach (var node in graph)
        {
            Assert.Contains(node, nodes);
        }

        var aEdges = graph.GetEdges('a');
        Assert.Equal(2, aEdges.Count);
        var aEdgesAsList = new List<(char, double)>();
        aEdgesAsList.Add((aEdges.Head!.Key.To, aEdges.Head!.Key.Weight));
        aEdgesAsList.Add((aEdges.Head.Next!.Key.To, aEdges.Head.Next!.Key.Weight));
        Assert.Contains(('b', 10), aEdgesAsList);
        Assert.Contains(('c', 20), aEdgesAsList);
    }
}
