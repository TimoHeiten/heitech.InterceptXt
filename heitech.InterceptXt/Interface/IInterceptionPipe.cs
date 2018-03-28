namespace heitech.InterceptXt.Interface
{
    public interface IInterceptionPipe<T>
    {
        void StartIntercept(T obj_to_intercept);
        void StartIntercept(IInterceptionContext context, T obj_to_intercept);

        void ForwardIntercept(T obj_to_intercept);
        void ForwardIntercept(IInterceptionContext context, T obj_to_intercept);

        void BackwardIntercept(T obj_to_intercept);
        void BackwardIntercept(IInterceptionContext context, T obj_to_intercept);
    }
}
