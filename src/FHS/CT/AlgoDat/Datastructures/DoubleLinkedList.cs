// AlgoDat Implementation
// Copyright (C) 2024  Fachhochschule Salzburg / Department Creative Technologies / Andreas Bilke

// This program is free software: you can redistribute it and/or modify
// it under the terms of the GNU General Public License as published by
// the Free Software Foundation, either version 3 of the License, or
// (at your option) any later version.

// This program is distributed in the hope that it will be useful,
// but WITHOUT ANY WARRANTY; without even the implied warranty of
// MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
// GNU General Public License for more details.

// You should have received a copy of the GNU General Public License
// along with this program.  If not, see <http://www.gnu.org/licenses/>.

using System.Collections;

namespace FHS.CT.AlgoDat.Datastructures
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
        
        /// <summary>Create an enumerator for the <cref="DoubleLinkedList"></summary>
        /// <remarks>
        /// yield can only be used to return an IEnumerator or and IEnumerable object.
        /// yield tells the compiler that the method it appears in is an "iteration block"
        /// In this case, we want to return the element of the list the Enumerator is
        /// currently "pointing at", as in: we iterate through our list
        /// with for each, the compiler needs to know what the element is we
        /// are currently looking at / working with.
        /// When just calling "M:AlgoDat:GetEnumerator()" the code will
        /// ACTUALLY NOT EXECUTE AT ALL!
        /// It simply returns an "C:System.Collections.Generic.IEnumerator<T>" object and stops!
        /// When calling the MoveNext() method (which is implemented by the IEnumerable interface
        /// and called every time we move to the next element in a (e.g.) for each),
        /// the code will execute, until it reaches the first yield. 
        /// It then simply returns the node.Key element - and stops executing this method.
        /// On the next call of MoveNext(), the execution takes off from where it left before -
        /// it will therefore keep going with line "node = node.Next;" until it reaches yield again.
        /// </remarks>
        /// <returns>An iterator over the double linked list</returns>
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