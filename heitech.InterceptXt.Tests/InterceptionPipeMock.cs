using heitech.InterceptXt.Interface;

namespace heitech.InterceptXt.Tests
{
    class InterceptionPipeMock : IInterceptionPipe
    {
        internal bool WasInvoked;
        public void BackwardIntercept()
            => WasInvoked = true;

        public void BackwardIntercept(IInterceptionContext context)
            => WasInvoked = true;

        public void ForwardIntercept()
            => WasInvoked = true;

        public void ForwardIntercept(IInterceptionContext context)
            => WasInvoked = true;

        public void StartIntercept()
            => WasInvoked = true;

        public void StartIntercept(IInterceptionContext context)
            => WasInvoked = true;
    }
}
