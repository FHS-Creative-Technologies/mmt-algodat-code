using FHS.CT.AlgoDat.DataStructures;

namespace Graph;

public class GraphTests
{
    [Fact]
    public void TestGraph()
    {
        var graph = new Graph<char>();
        graph.AddEdge('a', 'b');
        graph.AddEdge('a', 'c');
        graph.AddEdge('d', 'b');
        graph.AddEdge('c', 'b');

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
        Assert.Equal('b', aEdges.Search('b')!.Key);
        Assert.Equal('c', aEdges.Search('c')!.Key);
    }
}
