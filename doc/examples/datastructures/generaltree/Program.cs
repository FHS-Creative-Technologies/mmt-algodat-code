using FHS.CT.AlgoDat.DataStructures;

Tree<int> tree = new();
Tree<int>.Node root = new(5);
tree.Root = root;

Tree<int>.Node child1 = new(3);
root.Left = child1;

tree.InorderTreeWalk(root);
