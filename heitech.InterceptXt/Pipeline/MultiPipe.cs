using heitech.InterceptXt.Interface;
using heitech.ObjectXt.AttributeExtension;
using System.Collections.Generic;

namespace heitech.InterceptXt.Pipeline
{
    internal class MultiPipe<Tkey> : AttributeExtenderBase<Tkey>, IMultiPipe<Tkey>
    {
        public void BackwardIntercept(Tkey key) => GetPipe(key).BackwardIntercept();

        public void BackwardIntercept(Tkey key, IInterceptionContext context)
            => GetPipe(key).BackwardIntercept(context);

        public void ForwardIntercept(Tkey key)
            => GetPipe(key).ForwardIntercept();

        public void ForwardIntercept(Tkey key, IInterceptionContext context)
            => GetPipe(key).ForwardIntercept(context);

        public void StartIntercept(Tkey key)
            => GetPipe(key).StartIntercept();

        public void StartIntercept(Tkey key, IInterceptionContext context)
            => GetPipe(key).StartIntercept(context);

        public void Map(Tkey key, IInterceptionPipe pipe)
            => this[key] = pipe;

        private IInterceptionPipe GetPipe(Tkey key)
        {
            var val = this[key];
            if (val is IInterceptionPipe pipe)
                return pipe;
            else
                throw new KeyNotFoundException($"{key} was not mapped to multipipe. Register the key and its Pipeline with {nameof(Map)}");
        }
    }
}
