namespace heitech.InterceptXt.Interface
{
    public interface IMultiPipe<TKey>
    {
        void Map(TKey key, IInterceptionPipe pipe);

        void StartIntercept(TKey key);
        void StartIntercept(TKey key, IInterceptionContext context);

        void ForwardIntercept(TKey key);
        void ForwardIntercept(TKey key, IInterceptionContext context);

        void BackwardIntercept(TKey key);
        void BackwardIntercept(TKey key, IInterceptionContext context);
    }
}
