using heitech.InterceptXt.Interface;

namespace heitech.InterceptXt.Tests
{
    class InterceptorStub<T> : IIntercept<T>
    {
        internal IInterceptionContext Context;
        public void Invoke(IInterceptionContext context, T obj)
            => Context = context;
    }
}
