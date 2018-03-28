namespace heitech.InterceptXt.Interface
{
    public interface IMultiPipe<TKey, T>
    {
        void Map(TKey key, IInterceptionPipe<T> pipe);

        void Process(TKey key, T obj_to_intercept);
        void Process(TKey key, IInterceptionContext context, T obj_to_intercept);

        void Preprocess(TKey key, T obj_to_intercept);
        void Preprocess(TKey key, IInterceptionContext context, T obj_to_intercept);

        void Postprocess(TKey key, T obj_to_intercept);
        void Postprocess(TKey key, IInterceptionContext context, T obj_to_intercept);
    }
}
