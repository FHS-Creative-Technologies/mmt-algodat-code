using FHS.CT.AlgoDat.Algorithms;
using FHS.CT.AlgoDat.DataStructures;

namespace BreadthFirstSearch;

public class BreadthFirstSearchTests
{
    [Fact]
    public void TestBreadthFirstSearch()
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
        BreadthFirstSearch<char>.Travers(graph, 'a', delegate(char node)
        {
            visitedNodes.Add(node);
        });

        var expectedNodes = new List<char>()
        {
            'a',
            'b', 'c', // from a
            'd', 'f', // from b
            // none new from c
            // none new from d
            // none new from f
        };

        Assert.Equal(expectedNodes, visitedNodes);
    }
}
