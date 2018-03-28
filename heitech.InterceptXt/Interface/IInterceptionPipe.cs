namespace heitech.InterceptXt.Interface
{
    public interface IInterceptionPipe<T>
    {
        void Process(T obj_to_intercept);
        void Process(IInterceptionContext context, T obj_to_intercept);

        void Preprocess(T obj_to_intercept);
        void Preprocess(IInterceptionContext context, T obj_to_intercept);

        void Postprocess(T obj_to_intercept);
        void Postprocess(IInterceptionContext context, T obj_to_intercept);
    }
}
