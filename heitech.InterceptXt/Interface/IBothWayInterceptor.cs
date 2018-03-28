using System;
namespace heitech.InterceptXt.Interface
{
    public interface IBothWayInterceptor<T> : IIntercept<T>
    {
        void BackWardInvoke(IInterceptionContext context, T obj);
    }
}
