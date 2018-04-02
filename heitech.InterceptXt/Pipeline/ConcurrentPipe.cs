using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using heitech.InterceptXt.Context;
using heitech.InterceptXt.Interface;
using heitech.LinqXt.Enumerables;
using static heitech.ObjectXt.AttributeExtension.AttributeExtenderFactory;

namespace heitech.InterceptXt.Pipeline
{
    public class ConcurrentPipe<T> : IConcurrentInterceptionPipe<T>
    {
        public ConcurrentPipe(params IConcurrentIntercept<T>[] interceptors)
            :this(new InterceptorContext(Create<string>()), interceptors)
        { }

        private readonly IEnumerable<IConcurrentIntercept<T>> interceptors;
        private readonly IInterceptionContext context;

        public ConcurrentPipe(IInterceptionContext context, params IConcurrentIntercept<T>[] interceptors)
        {
            this.context = context;
            this.interceptors = interceptors;
        }

        public Task Process(T obj_to_intercept) => Process(this.context, obj_to_intercept);

        public Task Process(IInterceptionContext context, T obj_to_intercept)
        {
            var list = new List<Task>();
            interceptors.ForAll(x => list.Add(x.InvokeAsync(obj_to_intercept, context)));
            return Task.WhenAll(list);
        }
    }
}
