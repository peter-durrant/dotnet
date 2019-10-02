using System.Collections.Generic;
using System.Linq;

namespace Windowing
{
    public abstract class SimpleQueue<T> : ISimpleQueue<T>
    {
        private readonly Queue<T> _items = new Queue<T>();

        public void Add(T item)
        {
            _items.Enqueue(item);
        }

        public bool HasItems()
        {
            return _items.Any();
        }

        public T GetItem()
        {
            return _items.Dequeue();
        }
    }
}
