namespace BTree;

using FHS.CT.AlgoDat.DataStructures;

public class BTreeTests
{
    [Fact]
    public void TestInsert()
    {
        var btree = new BTree<int>(4);
        btree.Insert(1);
        btree.Insert(2);
        btree.Insert(3);
        btree.Insert(4);
        btree.Insert(5);
        btree.Insert(6);
        btree.Insert(7);

        KeysEqual(btree.Root, new [] { 1, 2, 3, 4, 5, 6, 7 });

        btree.Insert(8); // split at root happens here

        KeysEqual(btree.Root, new [] { 4 });
        KeysEqual(btree.Root.Children[0]!, new [] { 1, 2, 3 });
        KeysEqual(btree.Root.Children[1]!, new [] { 5, 6, 7, 8 });

        btree.Insert(9);
        btree.Insert(10);
        btree.Insert(11);

        KeysEqual(btree.Root, new [] { 4 });
        KeysEqual(btree.Root.Children[0]!, new [] { 1, 2, 3 });
        KeysEqual(btree.Root.Children[1]!, new [] { 5, 6, 7, 8, 9, 10, 11 });

        btree.Insert(12); // split in child. 8 bubbles up. Root has 3 children now
        KeysEqual(btree.Root, new [] { 4, 8 });
        KeysEqual(btree.Root.Children[0]!, new [] { 1, 2, 3 });
        KeysEqual(btree.Root.Children[1]!, new [] { 5, 6, 7 });
        KeysEqual(btree.Root.Children[2]!, new [] { 9, 10, 11, 12 });
    }

    [Fact]
    public void TestSearch()
    {
        var btree = new BTree<int>(4);
        for (var i = 0; i < 20; i++)
        {
            btree.Insert(i);
        }

        for (var i = 0; i < 20; i++)
        {
            var item = btree.Search(i);
            Assert.Equal(i, item);
        }

        var noItem = btree.Search(20); // this is not in list
        Assert.Equal(default, noItem);
    }

    [Fact]
    public void TestDelete()
    {
        var btree = CreateBookExampleTree();

        // should trigger case 1 (deletion in leaf)
        btree.Delete('F');

        KeysEqual(btree.Root, new [] { 'P' });
        KeysEqual(btree.Root.Children[0]!, new [] { 'C', 'G', 'M' });
        KeysEqual(btree.Root.Children[0]!.Children[1]!, new [] { 'D', 'E' });

        // should trigger case 2a. deletion in internal node, replace Ms position with predecessor
        // (which is L).
        btree.Delete('M');

        KeysEqual(btree.Root, new [] { 'P' });
        KeysEqual(btree.Root.Children[0]!, new [] { 'C', 'G', 'L' });
        KeysEqual(btree.Root.Children[0]!.Children[2]!, new [] { 'J', 'K' });

        // should trigger case 2c. deletion in internal node. Both children
        // have just t - 1 keys and needs to be merged
        btree.Delete('G');

        KeysEqual(btree.Root, new [] { 'P' });
        KeysEqual(btree.Root.Children[0]!, new [] { 'C', 'L' });
        KeysEqual(btree.Root.Children[0]!.Children[1]!, new [] { 'D', 'E', 'J', 'K' });
        KeysEqual(btree.Root.Children[0]!.Children[2]!, new [] { 'N', 'O' });

        // should trigger case 3b. With right sibling merge. It should end up at having an empty root during the operation
        btree.Delete('D');

        KeysEqual(btree.Root, new [] { 'C', 'L', 'P', 'T', 'X' });
        KeysEqual(btree.Root.Children[0]!, new [] { 'A', 'B' });
        KeysEqual(btree.Root.Children[1]!, new [] { 'E', 'J', 'K' });
        KeysEqual(btree.Root.Children[2]!, new [] { 'N', 'O' });
        KeysEqual(btree.Root.Children[3]!, new [] { 'Q', 'R', 'S' });
        KeysEqual(btree.Root.Children[4]!, new [] { 'U', 'V' });
        KeysEqual(btree.Root.Children[5]!, new [] { 'Y', 'Z' });

        // should trigger case 3a
        btree.Delete('B');
        KeysEqual(btree.Root, new [] { 'E', 'L', 'P', 'T', 'X' });
        KeysEqual(btree.Root.Children[0]!, new [] { 'A', 'C' });
        KeysEqual(btree.Root.Children[1]!, new [] { 'J', 'K' });
        KeysEqual(btree.Root.Children[2]!, new [] { 'N', 'O' });
        KeysEqual(btree.Root.Children[3]!, new [] { 'Q', 'R', 'S' });
        KeysEqual(btree.Root.Children[4]!, new [] { 'U', 'V' });
        KeysEqual(btree.Root.Children[5]!, new [] { 'Y', 'Z' });

        // should trigger case 3a with left sibling
        btree.Delete('U');
        KeysEqual(btree.Root, new [] { 'E', 'L', 'P', 'S', 'X' });
        KeysEqual(btree.Root.Children[0]!, new [] { 'A', 'C' });
        KeysEqual(btree.Root.Children[1]!, new [] { 'J', 'K' });
        KeysEqual(btree.Root.Children[2]!, new [] { 'N', 'O' });
        KeysEqual(btree.Root.Children[3]!, new [] { 'Q', 'R' });
        KeysEqual(btree.Root.Children[4]!, new [] { 'T', 'V' });
        KeysEqual(btree.Root.Children[5]!, new [] { 'Y', 'Z' });

        // should trigger case 3b with left sibling merge
        btree.Delete('J');
        KeysEqual(btree.Root, new [] { 'L', 'P', 'S', 'X' });
        KeysEqual(btree.Root.Children[0]!, new [] { 'A', 'C', 'E', 'K' });
        KeysEqual(btree.Root.Children[1]!, new [] { 'N', 'O' });
        KeysEqual(btree.Root.Children[2]!, new [] { 'Q', 'R' });
        KeysEqual(btree.Root.Children[3]!, new [] { 'T', 'V' });
        KeysEqual(btree.Root.Children[4]!, new [] { 'Y', 'Z' });

        var secondBtree = CreateSimpleTree();
        // should trigger 2b
        secondBtree.Delete('U');
        KeysEqual(secondBtree.Root, new [] { 'P', 'S', 'X' });
        KeysEqual(secondBtree.Root.Children[0]!, new [] { 'N', 'O' });
        KeysEqual(secondBtree.Root.Children[1]!, new [] { 'Q', 'R' });
        KeysEqual(secondBtree.Root.Children[2]!, new [] { 'T', 'V' });
        KeysEqual(secondBtree.Root.Children[3]!, new [] { 'Y', 'Z' });
    }

    // construct example from Cormen, Introduction to Algorithms, 4th Edition, Page 514
    // This code shall be hidden from any students eye!
    private static BTree<char> CreateBookExampleTree()
    {
        // insert the root by hand. The rest will be constructed directly with own objects
        // instead of inserting them
        var btree = new BTree<char>(3);
        btree.Insert('P');

        // === first level ===
        BTree<char>.Node firstChildFromRoot = new(3);
        firstChildFromRoot.Leaf = false;
        firstChildFromRoot.N = 3;
        firstChildFromRoot.Keys[0] = 'C';
        firstChildFromRoot.Keys[1] = 'G';
        firstChildFromRoot.Keys[2] = 'M';
        btree.Root.Children[0] = firstChildFromRoot;

        BTree<char>.Node secondChildFromRoot = new(3);
        secondChildFromRoot.Leaf = false;
        secondChildFromRoot.N = 2;
        secondChildFromRoot.Keys[0] = 'T';
        secondChildFromRoot.Keys[1] = 'X';
        btree.Root.Children[1] = secondChildFromRoot;

        btree.Root.Leaf = false;

        // === second level ===
        BTree<char>.Node firstChildFromFirstChild = new(3);
        firstChildFromFirstChild.N = 2;
        firstChildFromFirstChild.Keys[0] = 'A';
        firstChildFromFirstChild.Keys[1] = 'B';
        btree.Root.Children[0]!.Children[0] = firstChildFromFirstChild;

        BTree<char>.Node secondChildFromFirstChild = new(3);
        secondChildFromFirstChild.N = 3;
        secondChildFromFirstChild.Keys[0] = 'D';
        secondChildFromFirstChild.Keys[1] = 'E';
        secondChildFromFirstChild.Keys[2] = 'F';
        btree.Root.Children[0]!.Children[1] = secondChildFromFirstChild;

        BTree<char>.Node thirdChildFromFirstChild = new(3);
        thirdChildFromFirstChild.N = 3;
        thirdChildFromFirstChild.Keys[0] = 'J';
        thirdChildFromFirstChild.Keys[1] = 'K';
        thirdChildFromFirstChild.Keys[2] = 'L';
        btree.Root.Children[0]!.Children[2] = thirdChildFromFirstChild;

        BTree<char>.Node fourthChildFromFirstChild = new(3);
        fourthChildFromFirstChild.N = 2;
        fourthChildFromFirstChild.Keys[0] = 'N';
        fourthChildFromFirstChild.Keys[1] = 'O';
        btree.Root.Children[0]!.Children[3] = fourthChildFromFirstChild;

        // ======
        BTree<char>.Node firstChildFromSecondChild = new(3);
        firstChildFromSecondChild.N = 3;
        firstChildFromSecondChild.Keys[0] = 'Q';
        firstChildFromSecondChild.Keys[1] = 'R';
        firstChildFromSecondChild.Keys[2] = 'S';
        btree.Root.Children[1]!.Children[0] = firstChildFromSecondChild;

        BTree<char>.Node secondChildFromSecondChild = new(3);
        secondChildFromSecondChild.N = 2;
        secondChildFromSecondChild.Keys[0] = 'U';
        secondChildFromSecondChild.Keys[1] = 'V';
        btree.Root.Children[1]!.Children[1] = secondChildFromSecondChild;

        BTree<char>.Node thirdChildFromSecondChild = new(3);
        thirdChildFromSecondChild.N = 2;
        thirdChildFromSecondChild.Keys[0] = 'Y';
        thirdChildFromSecondChild.Keys[1] = 'Z';
        btree.Root.Children[1]!.Children[2] = thirdChildFromSecondChild;

        return btree;
    }

    private static BTree<char> CreateSimpleTree()
    {
        var btree = new BTree<char>(3);
        btree.Insert('P');
        btree.Insert('S');
        btree.Insert('U');
        btree.Root.Leaf = false;

        BTree<char>.Node firstChild = new(3);
        firstChild.Keys[0] = 'N';
        firstChild.Keys[1] = 'O';
        firstChild.N = 2;
        btree.Root.Children[0] = firstChild;

        BTree<char>.Node secondChild = new(3);
        secondChild.Keys[0] = 'Q';
        secondChild.Keys[1] = 'R';
        secondChild.N = 2;
        btree.Root.Children[1] = secondChild;

        BTree<char>.Node thirdChild = new(3);
        thirdChild.Keys[0] = 'T';
        thirdChild.Keys[1] = 'V';
        thirdChild.N = 2;
        btree.Root.Children[2] = thirdChild;

        BTree<char>.Node fourthChild = new(3);
        fourthChild.Keys[0] = 'X';
        fourthChild.Keys[1] = 'Y';
        fourthChild.Keys[2] = 'Z';
        fourthChild.N = 3;
        btree.Root.Children[3] = fourthChild;

        return btree;
    }

    private static void KeysEqual<T> (BTree<T>.Node node, T[] expectedEntries) where T : IComparable<T>
    {
        Assert.Equal(expectedEntries.Length, node.N);
        Assert.Equal(expectedEntries, node.Keys[..node.N]);
    }
}