using FHS.CT.AlgoDat.Algorithms;
using FHS.CT.AlgoDat.DataStructures;

namespace DepthFirstSearch;

public class DepthFirstSearchTests
{
    [Fact]
    public void TestDepthFirstSearch()
    {
        var graph = new Graph<char>();
        graph.AddEdge('a', 'b');
        graph.AddEdge('a', 'c');
        graph.AddEdge('b', 'd');
        graph.AddEdge('b', 'f');
        graph.AddEdge('c', 'b');
        graph.AddEdge('d', 'c');
        graph.AddEdge('e', 'c');
        graph.AddEdge('e', 'd');
        graph.AddEdge('f', 'd');

        var visitedNodes = new List<char>();
        DepthFirstSearch<char>.Travers(graph, 'a', delegate(char node)
        {
            visitedNodes.Add(node);
        });

        var expectedNodes = new List<char>()
        {
            'a',
            'b',
            'd',
            'c',
            'f'
        };

        Assert.Equal(expectedNodes, visitedNodes);
    }
}
