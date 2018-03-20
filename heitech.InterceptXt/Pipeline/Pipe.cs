using heitech.InterceptXt.Interface;
using heitech.LinqXt.Enumerables;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;

[assembly: InternalsVisibleTo("heitech.InterceptXt.Tests")]

namespace heitech.InterceptXt.Pipeline
{
    internal class Pipe : IInterceptionPipe
    {
        private readonly Action<IIntercept> endAction;
        private readonly IInterceptionContext fallback;
        private readonly Action<IIntercept> startingAction;
        private readonly IEnumerable<IIntercept> interceptors;

        public Pipe(IInterceptionContext fallback, Action<IIntercept> startingAction, params IIntercept[] interceptors)
            : this (fallback, startingAction, i => { }, interceptors)
        { }

        public Pipe(IInterceptionContext fallback, Action<IIntercept> start, Action<IIntercept> backpropagationAction, params IIntercept[] interceptors)
        {
            this.endAction = backpropagationAction;
            this.interceptors = interceptors;
            this.startingAction = start;
            this.fallback = fallback;
        }

        public void BackwardIntercept()
            => BackwardIntercept(fallback);

        public void BackwardIntercept(IInterceptionContext context)
        {
            var reversed = interceptors.Reverse();
            foreach (IIntercept interceptor in reversed)
            {
                endAction(interceptor);
                if (interceptor is IBothWayInterceptor bothways)
                    bothways.BackWardInvoke(context);
                else
                    interceptor.Invoke(context);
            }
        }

        public void ForwardIntercept()
            => ForwardIntercept(fallback);

        public void ForwardIntercept(IInterceptionContext context)
            => interceptors.ForAll(x =>
            {
                startingAction(x);
                x.Invoke(context);
            });

        public void StartIntercept()
            => StartIntercept(fallback);

        public void StartIntercept(IInterceptionContext context)
        {
            ForwardIntercept(context);
            BackwardIntercept(context);
        }
    }
}
