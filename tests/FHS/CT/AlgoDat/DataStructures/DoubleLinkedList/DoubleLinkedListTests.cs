namespace DoubleLinkedList;

using FHS.CT.AlgoDat.DataStructures;

public class DoubleLinkedListTests
{
    [Fact]
    public void TestInsert()
    {
        var list = new DoubleLinkedList<int>();

        list.Append(5);
        list.Append(2);
        list.Append(1);
        list.Append(9);

        Assert.Equal(4, list.Count);

        var node1 = list.Search(1);
        Assert.NotNull(node1);
        Assert.Equal(1, node1!.Key);

        var node4 = list.Search(4);
        Assert.Null(node4);
    }

    [Fact]
    public void TestDelete()
    {
        var list = new DoubleLinkedList<int>();

        list.Append(5);
        list.Append(2);
        list.Append(1);
        list.Append(9);

        Assert.Equal(4, list.Count);

        list.Delete(list.Search(1)!);
        Assert.Equal(3, list.Count);

        list.Delete(list.Search(5)!);
        Assert.Equal(2, list.Count);

        list.Delete(list.Search(2)!);
        Assert.Equal(1, list.Count);

        list.Delete(list.Search(9)!);
        Assert.Equal(0, list.Count);
    }
}