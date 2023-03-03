using AlgoDat;

using System.Text;

namespace AVLTree
{
    public static class AvlTreeDotPrinter<T> where T : IComparable<T>
    {
        public static string Print(AvlTree<T> tree)
        {
            StringBuilder sb = new();

            DotWalk(tree.Root!, sb);

            return "digraph {\ngraph [ordering=\"out\"];\n" + sb.ToString() + "}\n";
        }

        private static void DotWalk(AvlTree<T>.Node n, StringBuilder sb)
        {
            if (n == null)
            {
                return;
            }

            sb.Append($"{n.Key} [label=\"{n.Key} ({n.Height})\"];\n");

            if (n.Left != null)
            {
                DotWalk(n.Left, sb);
                sb.Append($"{n.Key} ->  {n.Left.Key};\n");
            }

            if (n.Right != null)
            {
                DotWalk(n.Right, sb);
                sb.Append($"{n.Key} ->  {n.Right.Key};\n");
            }
        }
    }
}