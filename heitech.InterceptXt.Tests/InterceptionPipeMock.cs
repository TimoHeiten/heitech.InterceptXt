using heitech.InterceptXt.Interface;

namespace heitech.InterceptXt.Tests
{
    class InterceptionPipeMock<T> : IInterceptionPipe<T>
    {
        internal bool WasInvoked;
        public void Postprocess(T obj)
            => WasInvoked = true;

        public void Postprocess(IInterceptionContext context, T obj)
            => WasInvoked = true;

        public void Preprocess(T obj)
            => WasInvoked = true;

        public void Preprocess(IInterceptionContext context, T obj)
            => WasInvoked = true;

        public void Process(T obj)
            => WasInvoked = true;

        public void Process(IInterceptionContext context, T obj)
            => WasInvoked = true;
    }
}
