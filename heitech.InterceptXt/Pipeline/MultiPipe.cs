using heitech.InterceptXt.Interface;
using heitech.ObjectXt.AttributeExtension;
using System.Collections.Generic;

namespace heitech.InterceptXt.Pipeline
{
    internal class MultiPipe<Tkey, T> : AttributeExtenderBase<Tkey>, IMultiPipe<Tkey, T>
    {
        public void BackwardIntercept(Tkey key, T obj) => GetPipe(key).BackwardIntercept(obj);

        public void BackwardIntercept(Tkey key, IInterceptionContext context, T obj)
            => GetPipe(key).BackwardIntercept(context, obj);

        public void ForwardIntercept(Tkey key, T obj)
            => GetPipe(key).ForwardIntercept(obj);

        public void ForwardIntercept(Tkey key, IInterceptionContext context, T obj)
            => GetPipe(key).ForwardIntercept(context, obj);

        public void StartIntercept(Tkey key, T obj)
            => GetPipe(key).StartIntercept(obj);

        public void StartIntercept(Tkey key, IInterceptionContext context, T obj)
            => GetPipe(key).StartIntercept(context, obj);

        public void Map(Tkey key, IInterceptionPipe<T> pipe)
            => this[key] = pipe;

        private IInterceptionPipe<T> GetPipe(Tkey key)
        {
            var val = this[key];
            if (val is IInterceptionPipe<T> pipe)
                return pipe;
            else
                throw new KeyNotFoundException($"{key} was not mapped to multipipe. Register the key and its Pipeline with {nameof(Map)}");
        }
    }
}
