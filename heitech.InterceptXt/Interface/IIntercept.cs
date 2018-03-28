namespace heitech.InterceptXt.Interface
{
    public interface IIntercept<T>
    {
        void Invoke(IInterceptionContext context, T obj_to_intercept);
    }
}
