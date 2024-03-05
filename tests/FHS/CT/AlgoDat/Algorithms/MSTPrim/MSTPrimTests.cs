using FHS.CT.AlgoDat.Algorithms;
using FHS.CT.AlgoDat.DataStructures;

namespace MSTKruskal;

public class MSTPrimTests
{
    [Fact]
    public void TestPrim()
    {
        var weightedGraph = new WeightedGraph<char>();

        weightedGraph.AddUndirectedEdge('a', 'b', 1d);
        weightedGraph.AddUndirectedEdge('a', 'd', 5);
        weightedGraph.AddUndirectedEdge('a', 'c', 4);

        weightedGraph.AddUndirectedEdge('b', 'd', 5);
        weightedGraph.AddUndirectedEdge('b', 'c', 3);

        weightedGraph.AddUndirectedEdge('c', 'f', 2);
        weightedGraph.AddUndirectedEdge('c', 'e', 8);

        weightedGraph.AddUndirectedEdge('d', 'f', 3);
        weightedGraph.AddUndirectedEdge('d', 'g', 4);
        weightedGraph.AddUndirectedEdge('d', 'e', 6);

        weightedGraph.AddUndirectedEdge('e', 'g', 1d);

        weightedGraph.AddUndirectedEdge('f', 'g', 3);

        var mst = MSTPrim<char>.CreateMinimumSpanningTree(weightedGraph, 'a');
        double totalWeight = 0;

        var exptectedEdges = new List<WeightedGraph<char>.Edge>
        {
            new('a', 'b', 1d),
            new('b', 'c', 3),
            new('d', 'f', 3),
            new('c', 'f', 2),
            new('e', 'g', 1),
            new('f', 'g', 3)
        };
        var expectedEdgesBack = new List<WeightedGraph<char>.Edge>();
        // add expected back links
        foreach (var edge in exptectedEdges)
        {
            expectedEdgesBack.Add(new(edge.To, edge.From, edge.Weight));
        }
        exptectedEdges.AddRange(expectedEdgesBack);
        foreach (var node in mst)
        {
            foreach (var edge in mst.GetEdges(node)!)
            {
                totalWeight += edge.Weight;

                Assert.Contains(edge, exptectedEdges);
            }
        }

        totalWeight /= 2;

        Assert.Equal(13, totalWeight);
    }
}
