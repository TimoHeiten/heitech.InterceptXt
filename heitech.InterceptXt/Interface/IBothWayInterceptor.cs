namespace heitech.InterceptXt.Interface
{
    public interface IBothWayInterceptor : IIntercept
    {
        void BackWardInvoke(IInterceptionContext context);
    }
}
