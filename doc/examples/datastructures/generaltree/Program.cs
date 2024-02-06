using FHS.CT.AlgoDat;

Tree<int> tree = new();
Tree<int>.Node<int> root = new(5);
tree.Root = root;

Tree<int>.Node<int> child1 = new(3);
root.Left = child1;

tree.InorderTreeWalk(root);