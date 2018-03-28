using heitech.InterceptXt.Interface;
using System;

namespace heitech.InterceptXt.Interceptors
{
    public class WrapperInterceptor<T> : IIntercept<T>
    {
        private readonly Action<IInterceptionContext, T> wrappedAction;

        public WrapperInterceptor(Action<IInterceptionContext, T> wrappedAction)
            => this.wrappedAction = wrappedAction;

        public void Invoke(IInterceptionContext context, T obj)
            => wrappedAction(context, obj);
        
    }

    public class WrapperBothWayInterceptor<T> : WrapperInterceptor<T>, IBothWayInterceptor<T>
    {

        private readonly Action<IInterceptionContext, T> backwardAction;

        public WrapperBothWayInterceptor(Action<IInterceptionContext, T> wrappedAction, Action<IInterceptionContext, T> backwardAction) : base(wrappedAction) 
            => this.backwardAction = backwardAction;

        public void BackWardInvoke(IInterceptionContext context, T obj)
            => backwardAction(context, obj);
    }
}
