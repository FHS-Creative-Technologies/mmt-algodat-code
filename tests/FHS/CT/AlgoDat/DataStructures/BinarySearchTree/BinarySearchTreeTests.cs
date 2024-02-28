namespace BinarySearchTree;

using FHS.CT.AlgoDat.DataStructures;

public class BinarySearchTreeTests
{
    [Fact]
    public void TestInsert()
    {
        var st = new BinarySearchTree<int>();
        st.Insert(6);
        st.Insert(8);
        st.Insert(10);
        st.Insert(7);

        var node = st.Root!;

        Assert.Equal(6, node.Key);
        Assert.Null(node.Left);
        Assert.Equal(8, node.Right!.Key);
        Assert.Equal(7, node.Right!.Left!.Key);
        Assert.Equal(10, node.Right!.Right!.Key);
    }

    [Fact]
    public void TestSearch()
    {
        var st = new BinarySearchTree<int>();
        st.Insert(6);
        st.Insert(8);
        st.Insert(10);
        st.Insert(7);

        var node7 = st.Search(7);
        Assert.NotNull(node7);
        Assert.Equal(7, node7!.Key);
    }

    [Fact]
    public void TestDeleteTwoChildren()
    {
        var st = new BinarySearchTree<int>();
        st.Insert(6);
        st.Insert(5);
        st.Insert(8);
        st.Insert(10);
        st.Insert(7);

        st.Delete(st.Search(6)!); // delete the root element
        Assert.Null(st.Search(6)); // 6 should be gone

        // now the new root should be 7 (as successor)
        var node = st.Root;
        Assert.Equal(7, node!.Key);
        Assert.Equal(8, node.Right!.Key);
        Assert.Equal(10, node.Right.Right!.Key);
        Assert.Null(node.Right.Left); // before 7 was left child of 8
    }

    [Fact]
    public void TestDeleteOnlyOne()
    {
        var st = new BinarySearchTree<int>();
        st.Insert(6);
        st.Insert(8);
        st.Insert(10);
        st.Insert(7);

        st.Delete(st.Search(6)!); // delete the root element
        Assert.Null(st.Search(6)); // 6 should be gone

        // now the new root should be 7 (as successor)
        var node = st.Root;
        Assert.Equal(8, node!.Key);
        Assert.Equal(10, node.Right!.Key);
        Assert.Equal(7, node.Left!.Key);
    }

    [Fact]
    public void TestDeleteSuccessorInParentTree()
    {
        var st = new BinarySearchTree<int>();
        st.Insert(1);
        st.Insert(5);
        st.Insert(10);
        st.Insert(3);
        st.Insert(2);

        st.Delete(st.Search(3)!); // delete somewhere in the sub tree
        Assert.Null(st.Search(3)); // 3 should be gone

        var node = st.Root;
        Assert.Equal(1, node!.Key);
        Assert.Equal(5, node.Right!.Key);
        Assert.Equal(10, node.Right.Right!.Key);
        Assert.Equal(2, node.Right.Left!.Key);
    }
}