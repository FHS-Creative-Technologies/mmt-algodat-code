namespace AVLTree;

using FHS.CT.AlgoDat.DataStructures;

public class AVLTreeTests
{
    [Fact]
    public void TestDelete()
    {
        AvlTree<int> tree = new();
        List<int> insertedNumbers = new();
        Random r = new();

        while (insertedNumbers.Count <= 500)
        {
            var number = r.Next(20000);
            tree.Insert(number);
            insertedNumbers.Add(number);
        }

        AvlTreeChecker<int>.Check(tree);

        int counter = 0;
        while (counter <= 40)
        {
            var number = insertedNumbers[r.Next(insertedNumbers.Count - 1)];
            var node = tree.Search(number);

            tree.Delete(node);
            insertedNumbers.Remove(number);
            AvlTreeChecker<int>.Check(tree);

            counter++;
        }
    }

    [Fact]
    public void TestInsert()
    {
        AvlTree<int> tree = new();

        Random r = new();

        var counter = 0;
        while (counter <= 1000)
        {
            var number = r.Next();
            tree.Insert(number);

            counter++;
        }

        AvlTreeChecker<int>.Check(tree);
    }
}

public static class AvlTreeChecker<T> where T : IComparable<T>
{
    public static void Check(AvlTree<T> tree)
    {
        CheckWalk(tree, tree.Root!);
    }

    private static int CheckWalk(AvlTree<T> tree, AvlTree<T>.Node node)
    {
        if (node == tree.Root)
        {
            Assert.Null(node.Parent);
        }

        var storedLeftHeight = 0;
        var computedLeftHeight = 0;
        if (node.Left != null)
        {
            Assert.True(node.Left.Key.CompareTo(node.Key) < 0);
            Assert.Equal(node.Left.Parent, node);

            storedLeftHeight = node.Left.Height;
            computedLeftHeight = CheckWalk(tree, node.Left);
        }

        var storedRightHeight = 0;
        var computedRightHeight = 0;
        if (node.Right != null)
        {
            Assert.True(node.Right.Key.CompareTo(node.Key) >= 0);
            Assert.Equal(node.Right.Parent, node);

            storedRightHeight = node.Right.Height;
            computedRightHeight = CheckWalk(tree, node.Right);
        }

        Assert.Equal(Math.Max(storedRightHeight, storedLeftHeight) + 1, node.Height);

        var maxComputedHeight = Math.Max(computedRightHeight, computedLeftHeight);
        Assert.Equal(node.Height, maxComputedHeight + 1);

        return maxComputedHeight + 1;
    }
}