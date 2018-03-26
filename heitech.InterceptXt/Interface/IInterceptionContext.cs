namespace heitech.InterceptXt.Interface
{
    public interface IInterceptionContext
    {
        object this[string val] { get;set; }

        bool TryGet<T>(string key, out T t);
        void Set<T>(string key, object value, bool _override = true);
    }

    public interface IInterceptionContext<K, T>
    {
        T this[K key] { get; set; }

        bool TryGet(K key, out T t);
        void Set(K key, T value, bool _override = true);
    }
}
