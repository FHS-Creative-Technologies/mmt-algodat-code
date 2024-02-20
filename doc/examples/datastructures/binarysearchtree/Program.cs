using FHS.CT.AlgoDat.DataStructures;

BinarySearchTree<int> tree = new();
tree.Insert(4);
tree.Insert(3);
tree.Insert(6);
tree.Insert(5);
tree.Insert(8);
tree.Insert(7);

var node7 = tree.Search(7);
Console.WriteLine($"Found node {node7!.Key}"); // if node7 is null, we would be doomed!

var node6 = tree.Search(6);
tree.Delete(node6!); // if node6 is null, we would be doomed