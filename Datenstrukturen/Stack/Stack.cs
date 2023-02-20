using System;

namespace AlgoDat
{
    public class Stack<T> where T : IComparable<T>
    {
        private DoubleLinkedList<T> _list;

        public int Count
        {
            get
            {
                return _list.Count;
            }
        }

        public Stack()
        {
            _list = new();
        }

        public void Push(T elem)
        {
            _list.Append(elem);
        }

        public T Pop()
        {
            DoubleLinkedList<T>.Node<T> elem = _list.Tail;
            _list.Delete(_list.Tail);

            return elem.Key;
        }
    }
}