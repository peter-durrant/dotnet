namespace Windowing
{
    public interface ISimpleQueue<T>
    {
        void Add(T item);
        bool HasItems();
        T GetItem();
    }
}
