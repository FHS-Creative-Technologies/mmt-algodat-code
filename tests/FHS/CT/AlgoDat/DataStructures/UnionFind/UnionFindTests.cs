using FHS.CT.AlgoDat.DataStructures;

namespace UnionFind;

public class UnionFindTests
{
    [Fact]
    public void TestUnionFind()
    {
        var unionFind = new UnionFind<int>();
        unionFind.MakeSet(1);
        unionFind.MakeSet(2);
        unionFind.MakeSet(3);
        unionFind.MakeSet(4);
        unionFind.MakeSet(5);

        unionFind.Union(1, 2); // set is 1,2
        Assert.Equal(1, unionFind.Find(2));
        unionFind.Union(3, 4); // set is 3,4
        Assert.Equal(3, unionFind.Find(4));

        unionFind.Union(2, 4); // set is 1,2,3,4
        Assert.Equal(1, unionFind.Find(4));
    }
}
