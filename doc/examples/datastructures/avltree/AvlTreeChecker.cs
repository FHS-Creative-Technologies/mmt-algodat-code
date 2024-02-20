using FHS.CT.AlgoDat.Datastructures;

using System.Diagnostics;

namespace AVLTree
{
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
                Debug.Assert(node.Parent == null);
            }

            int storedLeftHeight = 0;
            int computedLeftHeight = 0;
            if (node.Left != null)
            {
                Debug.Assert(node.Left.Key.CompareTo(node.Key) < 0);
                Debug.Assert(node.Left.Parent == node);

                storedLeftHeight = node.Left.Height;

                computedLeftHeight = CheckWalk(tree, node.Left);
            }

            int storedRightHeight = 0;
            int computedRightHeight = 0;
            if (node.Right != null)
            {
                Debug.Assert(node.Right.Key.CompareTo(node.Key) >= 0);
                Debug.Assert(node.Right.Parent == node);

                storedRightHeight = node.Right.Height;
                computedRightHeight = CheckWalk(tree, node.Right);
            }

            Debug.Assert(node.Height == Math.Max(storedRightHeight, storedLeftHeight) + 1);

            int maxComputedHeight = Math.Max(computedRightHeight, computedLeftHeight);
            Debug.Assert(maxComputedHeight + 1 == node.Height);

            return maxComputedHeight + 1;
        }
    }
}