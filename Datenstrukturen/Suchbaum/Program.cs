using AlgoDat;

using System;

Tree<int> tree = new();
tree.Insert(4);
tree.Insert(3);
tree.Insert(6);
tree.Insert(5);
tree.Insert(8);
tree.Insert(7);

var node7 = tree.Search(7);
if(node7 is not null)
    Console.WriteLine($"Found node {node7.Key}");

var node6 = tree.Search(6);
if(node6 is not null)
    tree.Delete(node6);