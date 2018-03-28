namespace heitech.InterceptXt.Interface
{
    public interface IMultiPipe<TKey, T>
    {
        void Map(TKey key, IInterceptionPipe<T> pipe);

        void StartIntercept(TKey key, T obj_to_intercept);
        void StartIntercept(TKey key, IInterceptionContext context, T obj_to_intercept);

        void ForwardIntercept(TKey key, T obj_to_intercept);
        void ForwardIntercept(TKey key, IInterceptionContext context, T obj_to_intercept);

        void BackwardIntercept(TKey key, T obj_to_intercept);
        void BackwardIntercept(TKey key, IInterceptionContext context, T obj_to_intercept);
    }
}
