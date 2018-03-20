using heitech.InterceptXt.Interface;

namespace heitech.InterceptXt.Tests
{
    internal class BackwardInterceptorStub : IBothWayInterceptor
    {
        internal IInterceptionContext Context;
        internal bool WasInvokedDirectly;
        public void BackWardInvoke(IInterceptionContext context)
        {
            Context = context;
            WasInvokedDirectly = true;
        }

        public void Invoke(IInterceptionContext context)
            => Context = context;
    }
}
