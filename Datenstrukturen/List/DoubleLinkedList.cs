using System;
using System.Collections;
using System.Collections.Generic;

namespace AlgoDat
{
    public class DoubleLinkedList<T> : IEnumerable<T> where T : IComparable<T>
    {
        public class Node<U>
        {
            public Node<U> Prev { get; set; }
            public Node<U> Next { get; set; }
            public U Key { get; }

            public Node(U key)
            {
                Key = key;
            }
        }

        public Node<T> Head { get; private set; }
        public Node<T> Tail { get; private set; }

        private int _counter;

        public int Count
        {
            get
            {
                return _counter;
            }

            private set
            {
                _counter = value;
            }
        }

        public DoubleLinkedList()
        {
            Head = null;
            Tail = null;
        
            _counter = 0;
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }

        public IEnumerator<T> GetEnumerator()
        {
            return new DoubleLinkedListEnumerator(Head);
        }

        public class DoubleLinkedListEnumerator : IEnumerator<T>
        {
            private Node<T> _currentNode;
            private bool _firstNode;

            public T Current
            {
                get
                {
                    return _currentNode != null ? _currentNode.Key : default(T);
                }
            }

            object IEnumerator.Current
            {
                get
                {
                    return _currentNode != null ? _currentNode.Key : default(T);
                }
            }

            public DoubleLinkedListEnumerator(Node<T> startNode)
            {
                _currentNode = startNode;
                _firstNode = true;
            }

            public bool MoveNext()
            {
                if (_currentNode == null)
                {
                    return false;
                }

                // Do not move the pointer forward if we just started
                // with enumeration. Otherwise we would skip the first item
                if (_firstNode)
                {
                    _firstNode = false;
                }
                else
                {
                    _currentNode = _currentNode.Next;
                }

                return _currentNode != null;
            }

            public void Reset()
            {
                _currentNode = null;
            }

            public void Dispose()
            {
                _currentNode = null;
            }
        }

        public Node<T> Append(T key)
        {
            Node<T> newNode = new(key);

            newNode.Prev = Tail;
            if (Tail != null)
            {
                Tail.Next = newNode;
            }
            Tail = newNode;
            if (Head == null)
            {
                Head = newNode;
            }

            Count++;

            return newNode;
        }

        public Node<T> Search(T key)
        {
            Node<T> current = Head;
            while (current != null && current.Key.CompareTo(key) != 0)
            {
                current = current.Next;
            }

            return current;
        }

        public void Delete(Node<T> node)
        {
            if (node.Prev != null)
            {
                node.Prev.Next = node.Next;
            }
            else
            {
                Head = node.Next;
            }

            if (node.Next != null)
            {
                node.Next.Prev = node.Prev;
            }
            else
            {
                Tail = node.Prev;
            }

            Count--;
        }
    }
}