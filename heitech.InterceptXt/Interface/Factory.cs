using heitech.InterceptXt.Context;
using heitech.InterceptXt.Pipeline;
using heitech.ObjectXt.AttributeExtension;
using System;

namespace heitech.InterceptXt.Interface
{
    public static class Factory
    {
        public static IInterceptionPipe<T> Create<T>(params IIntercept<T>[] interceptors) 
            => Create<T>(_ => { }, interceptors);

        public static IInterceptionPipe<T> Create<T>(Action<IIntercept<T>> action, params IIntercept<T>[] interceptors) 
            => Create<T>(action, _ => { }, interceptors);

        public static IInterceptionPipe<T> Create<T>(Action<IIntercept<T>> action, Action<IIntercept<T>> backwardAction, params IIntercept<T>[] interceptors)
            => Create<T>(action, backwardAction, GetContext(), interceptors);

        public static IInterceptionPipe<T> Create<T>(Action<IIntercept<T>> action, Action<IIntercept<T>> backwardAction, IInterceptionContext _defaultContext, params IIntercept<T>[] interceptors)
            => new Pipe<T>(_defaultContext, action, backwardAction, interceptors);

        private static IInterceptionContext GetContext() 
            => new InterceptorContext(AttributeExtenderFactory.Create<string>());

        //##### multipipe
        public static IMultiPipe<Key, T> CreateMultiPipe<Key, T>()
            => new MultiPipe<Key, T>();
    }
}
