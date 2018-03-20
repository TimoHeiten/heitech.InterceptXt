using heitech.InterceptXt.Context;
using heitech.InterceptXt.Pipeline;
using heitech.ObjectExpander.AttributeExtension;
using System;

namespace heitech.InterceptXt.Interface
{
    public static class Factory
    {
        public static IInterceptionPipe Create(params IIntercept[] interceptors) 
            => Create(_ => { }, interceptors);

        public static IInterceptionPipe Create(Action<IIntercept> action, params IIntercept[] interceptors) 
            => Create(action, _ => { }, interceptors);

        public static IInterceptionPipe Create(Action<IIntercept> action, Action<IIntercept> backwardAction, params IIntercept[] interceptors)
            => Create(action, backwardAction, GetContext(), interceptors);

        public static IInterceptionPipe Create(Action<IIntercept> action, Action<IIntercept> backwardAction, IInterceptionContext _defaultContext, params IIntercept[] interceptors)
            => new Pipe(_defaultContext, action, backwardAction, interceptors);

        private static IInterceptionContext GetContext() 
            => new InterceptorContext(AttributeExtenderFactory.Create<string>());
    }
}
