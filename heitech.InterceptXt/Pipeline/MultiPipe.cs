using heitech.InterceptXt.Interface;
using heitech.ObjectXt.AttributeExtension;
using System.Collections.Generic;

namespace heitech.InterceptXt.Pipeline
{
    internal class MultiPipe<Tkey, T> : AttributeExtenderBase<Tkey>, IMultiPipe<Tkey, T>
    {
        public void Postprocess(Tkey key, T obj) => GetPipe(key).Postprocess(obj);

        public void Postprocess(Tkey key, IInterceptionContext context, T obj)
            => GetPipe(key).Postprocess(obj);

        public void Preprocess(Tkey key, T obj)
            => GetPipe(key).Preprocess(obj);

        public void Preprocess(Tkey key, IInterceptionContext context, T obj)
            => GetPipe(key).Preprocess(context, obj);

        public void Process(Tkey key, T obj)
            => GetPipe(key).Process(obj);

        public void Process(Tkey key, IInterceptionContext context, T obj)
            => GetPipe(key).Process(context, obj);

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
