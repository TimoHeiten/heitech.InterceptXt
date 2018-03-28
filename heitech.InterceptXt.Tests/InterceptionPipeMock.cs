using heitech.InterceptXt.Interface;

namespace heitech.InterceptXt.Tests
{
    class InterceptionPipeMock<T> : IInterceptionPipe<T>
    {
        internal bool WasInvoked;
        public void BackwardIntercept(T obj)
            => WasInvoked = true;

        public void BackwardIntercept(IInterceptionContext context, T obj)
            => WasInvoked = true;

        public void ForwardIntercept(T obj)
            => WasInvoked = true;

        public void ForwardIntercept(IInterceptionContext context, T obj)
            => WasInvoked = true;

        public void StartIntercept(T obj)
            => WasInvoked = true;

        public void StartIntercept(IInterceptionContext context, T obj)
            => WasInvoked = true;
    }
}
