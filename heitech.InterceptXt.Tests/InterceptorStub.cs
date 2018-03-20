using heitech.InterceptXt.Interface;

namespace heitech.InterceptXt.Tests
{
    class InterceptorStub : IIntercept
    {
        internal IInterceptionContext Context;
        public void Invoke(IInterceptionContext context)
            => Context = context;
    }
}
