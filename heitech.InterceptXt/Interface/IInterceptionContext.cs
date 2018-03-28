namespace heitech.InterceptXt.Interface
{
    public interface IInterceptionContext
    {
        object this[string val] { get;set; }

        bool TryGet<T>(string key, out T t);
        void Set<T>(string key, object value, bool _override = true);
    }
}
