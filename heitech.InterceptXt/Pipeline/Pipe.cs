using heitech.InterceptXt.Interface;
using heitech.LinqXt.Enumerables;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;

[assembly: InternalsVisibleTo("heitech.InterceptXt.Tests")]

namespace heitech.InterceptXt.Pipeline
{
    internal class Pipe<T> : IInterceptionPipe<T>
    {
        private readonly Action<IIntercept<T>> endAction;
        private readonly IInterceptionContext fallback;
        private readonly Action<IIntercept<T>> startingAction;
        private readonly IEnumerable<IIntercept<T>> interceptors;

        public Pipe(IInterceptionContext fallback, Action<IIntercept<T>> startingAction, params IIntercept<T>[] interceptors)
            : this (fallback, startingAction, i => { }, interceptors)
        { }

        public Pipe(IInterceptionContext fallback, Action<IIntercept<T>> start, Action<IIntercept<T>> backpropagationAction, params IIntercept<T>[] interceptors)
        {
            this.endAction = backpropagationAction;
            this.interceptors = interceptors;
            this.startingAction = start;
            this.fallback = fallback;
        }

        public void BackwardIntercept(T obj)
            => BackwardIntercept(fallback, obj);

        public void BackwardIntercept(IInterceptionContext context, T obj)
        {
            var reversed = interceptors.Reverse();
            foreach (IIntercept<T> interceptor in reversed)
            {
                endAction(interceptor);
                if (interceptor is IBothWayInterceptor<T> bothways)
                    bothways.BackWardInvoke(context, obj);
                else
                    interceptor.Invoke(context, obj);
            }
        }

        public void ForwardIntercept(T obj)
            => ForwardIntercept(fallback, obj);

        public void ForwardIntercept(IInterceptionContext context, T obj)
            => interceptors.ForAll(x =>
            {
                startingAction(x);
                x.Invoke(context, obj);
            });

        public void StartIntercept(T obj)
            => StartIntercept(fallback, obj);

        public void StartIntercept(IInterceptionContext context, T obj)
        {
            ForwardIntercept(context, obj);
            BackwardIntercept(context, obj);
        }
    }
}
