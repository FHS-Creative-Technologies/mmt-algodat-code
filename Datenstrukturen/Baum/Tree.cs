using System;

namespace AlgoDat
{
    class Tree<T>
    {
        public class Node<U>
        {
            public T Key { get; }

            public Node<U> Left { get; set; }
            public Node<U> Right { get; set; }

            public Node(T key)
            {
                Key = key;
            }
        }

        public Node<T> Root { get; set; }

        public void InorderTreeWalk(Node<T> node)
        {
            if (node == null)
            {
                return;
            }

            InorderTreeWalk(node.Left);
            Console.WriteLine(node.Key);
            InorderTreeWalk(node.Right);
        }
    }
}