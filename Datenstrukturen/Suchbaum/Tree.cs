using System;

namespace AlgoDat
{
    public class Tree<T> where T : IComparable<T>
    {
        public class Node<U>
        {
            public T Key { get; }

            public Node<U>? Left { get; set; }
            public Node<U>? Right { get; set; }
            public Node<U>? Parent { get; set; }

            public Node(T key)
            {
                Key = key;
            }
        }

        public Node<T>? Root { get; set; }

        public Node<T>? Search(T searchKey)
        {
            return Search(searchKey, Root);
        }

        private Node<T>? Search(T searchKey, Node<T>? currentNode)
        {
            while (currentNode != null && searchKey.CompareTo(currentNode.Key) != 0)
            {
                if (searchKey.CompareTo(currentNode.Key) < 0)
                {
                    currentNode = currentNode.Left;
                }
                else
                {
                    currentNode = currentNode.Right;
                }
            }

            return currentNode;
        }

        public Node<T> Insert(T key)
        {
            Node<T>? currentNode = Root;
            Node<T>? lastParent = null;

            while (currentNode != null)
            {
                lastParent = currentNode;
                if (key.CompareTo(currentNode.Key) < 0)
                {
                    currentNode = currentNode.Left;
                }
                else
                {
                    currentNode = currentNode.Right;
                }
            }

            Node<T> newNode = new(key);
            newNode.Parent = lastParent;
            if (lastParent == null)
            {
                Root = newNode;
            }
            else if (newNode.Key.CompareTo(lastParent.Key) < 0)
            {
                lastParent.Left = newNode;
            }
            else
            {
                lastParent.Right = newNode;
            }

            return newNode;
        }

        private Node<T> Minimum(Node<T> n)
        {
            while (n.Left != null)
            {
                n = n.Left;
            }

            return n;
        }

        private Node<T>? Successor(Node<T> n)
        {
            if (n.Right != null)
            {
                return Minimum(n.Right);
            }

            Node<T>? parent = n.Parent;
            while (parent != null && n == parent.Right)
            {
                n = parent;
                parent = parent.Parent;
            }

            return parent;
        }

        private void Transplant(Node<T>? u, Node<T>? v)
        {
            if (u is null)
            {
                return;
            }
            
            if (u.Parent == null)
            {
                Root = v;
            }
            else if (u == u.Parent.Left)
            {
                u.Parent.Left = v;
            }
            else
            {
                u.Parent.Right = v;
            }

            if (v != null)
            {
                v.Parent = u.Parent;
            }
        }

        public void Delete(Node<T> d)
        {
            if (d.Left == null)
            {
                Transplant(d, d.Right);
            }
            else if (d.Right == null)
            {
                Transplant(d, d.Left);
            }
            else
            {
                Node<T>? min = Successor(d);
                if (min is not null)
                {
                    if (min.Parent != d)
                    {
                        Transplant(min, min.Right!);
                        min.Right = d.Right;
                        min.Right.Parent = min;
                    }
                    Transplant(d, min);
                    min.Left = d.Left;
                    min.Left.Parent = min;
                }
            }
        }
    }
}