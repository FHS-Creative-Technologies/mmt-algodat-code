using System;
using System.Collections;
using System.Collections.Generic;

namespace AlgoDat
{
    public class DoubleLinkedList<T> : IEnumerable<T> where T : IComparable<T>
    {
        public class Node<U>
        {
            public Node<U>? Prev { get; set; }
            public Node<U>? Next { get; set; }
            public U Key { get; }

            public Node(U key)
            {
                Key = key;
            }
        }

        public Node<T>? Head { get; private set; }
        public Node<T>? Tail { get; private set; }

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
        
        // -------Explanation keyword: yield-------
        // yield can only be used to return an IEnumerator or and IEnumerable object.
        // yield tells the compiler that the method it appears in is an "iteration block"
        // In this case, we want to return the element of the list the Enumerator is
        // currently "pointing at", as in: we iterate through our list
        // with for each, the compiler needs to know what the element is we
        // are currently looking at / working with.
        // When just calling GetEnumerator() the code will
        // ACTUALLY NOT EXECUTE AT ALL!
        // It simply returns an IEnumerator<T> object and stops!
        // When calling the MoveNext() method (which is implemented by the IEnumerable interface
        // and called every time we move to the next element in a (f.ex.) for each),
        // the code will execute, until it reaches the first yield. 
        // It then simply returns the node.Key element - and stops executing this method.
        // On the next call of MoveNext(), the execution takes off from where it left before -
        // it will therefore keep going with line "node = node.Next;" until it reaches yield again.
        public IEnumerator<T> GetEnumerator()
        {
            var node = Head;
            while(node != null)
            {
                yield return node.Key;
                node = node.Next;
            }
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
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

        public Node<T>? Search(T key)
        {
            Node<T>? current = Head;
            while (current is not null && current.Key.CompareTo(key) != 0)
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