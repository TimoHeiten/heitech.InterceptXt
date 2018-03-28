using heitech.InterceptXt.Interface;

namespace heitech.InterceptXt.Tests
{
    internal class BackwardInterceptorStub<T> : IBothWayInterceptor<T>
    {
        internal IInterceptionContext Context;
        internal bool WasInvokedDirectly;
        public void BackWardInvoke(IInterceptionContext context, T obj)
        {
            Context = context;
            WasInvokedDirectly = true;
        }

        public void Invoke(IInterceptionContext context, T obj)
            => Context = context;
    }
}
