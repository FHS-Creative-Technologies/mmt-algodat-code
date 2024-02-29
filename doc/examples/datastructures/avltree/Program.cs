using AVLTree;
using FHS.CT.AlgoDat.DataStructures;


var tree = new AvlTree<int>();

for (var i = 0; i < 100; i++)
{
    tree.Insert(i);
}

Console.WriteLine(AvlTreeDotPrinter<int>.Print(tree));