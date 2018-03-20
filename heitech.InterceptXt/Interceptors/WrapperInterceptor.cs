using heitech.InterceptXt.Interface;
using System;

namespace heitech.InterceptXt.Interceptors
{
    public class WrapperInterceptor : IIntercept
    {
        private readonly Action<IInterceptionContext> wrappedAction;

        public WrapperInterceptor(Action<IInterceptionContext> wrappedAction)
            => this.wrappedAction = wrappedAction;

        public void Invoke(IInterceptionContext context)
            => wrappedAction(context);
        
    }

    public class WrapperBothWayInterceptor : WrapperInterceptor, IBothWayInterceptor
    {

        private readonly Action<IInterceptionContext> backwardAction;

        public WrapperBothWayInterceptor(Action<IInterceptionContext> wrappedAction, Action<IInterceptionContext> backwardAction) : base(wrappedAction) 
            => this.backwardAction = backwardAction;

        public void BackWardInvoke(IInterceptionContext context)
            => backwardAction(context);
    }
}
