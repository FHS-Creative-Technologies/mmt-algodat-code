using FHS.CT.AlgoDat.Datastructures;

// testing insert
{
    var tree = new BTree<int>(4);
    tree.Insert(4);
    tree.Insert(3);
    tree.Insert(6);
    tree.Insert(5);
    tree.Insert(8);
    tree.Insert(7);
    tree.Insert(1);
    tree.Insert(-1); // this should trigger creating a new node
    tree.Insert(-5);
    tree.Insert(12);
    tree.Insert(24);

    var node7 = tree.Search(7);
    Console.WriteLine($"Found node {node7!}"); // if node7 is null, we would be doomed!
}

// testing delete
{
    var tree = new BTree<int>(4);
    tree.Insert(4);
    tree.Insert(3);
    tree.Insert(6);
    tree.Insert(5);
    tree.Insert(8);
    tree.Insert(7);
    tree.Insert(1);

    tree.Delete(6); // this delete should happen in root which is a Leaf for now
}

// testing random insert
{
    Console.WriteLine("Adding random numbers");
    var tree = new BTree<int>(4);
    var random = new Random();

    var drawNumbers = new List<int>();
    for (var i = 0; i < 1000000; i++)
    {
        var n = random.Next();
        drawNumbers.Add(n);
        tree.Insert(n);
    }

    var searchNumber = drawNumbers[random.Next(drawNumbers.Count)];
    var item = tree.Search(searchNumber);
    Console.WriteLine($"Found node {item!}");
}



