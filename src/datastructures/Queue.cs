namespace FHS.CT.AlgoDat
{
    public class Queue<T> where T : IComparable<T>
    {
        private DoubleLinkedList<T> _list;

        public int Count
        {
            get
            {
                return _list.Count;
            }
        }

        public Queue()
        {
            _list = new();
        }

        public void Enqueue(T elem)
        {
            _list.Append(elem);
        }

        public T? Dequeue()
        {
            DoubleLinkedList<T>.Node<T>? elem = _list.Head;

            if(elem is null)
            {
                return default(T);
            }

            _list.Delete(elem);

            return elem.Key;
        }
    }
}
