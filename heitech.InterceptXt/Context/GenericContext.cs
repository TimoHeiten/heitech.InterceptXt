using heitech.InterceptXt.Interface;
using heitech.ObjectXt.Interfaces;
using System.Diagnostics.CodeAnalysis;

namespace heitech.InterceptXt.Context
{
    // only a wrapper around AttributeExtender
    [ExcludeFromCodeCoverage]
    public class GenericContext<K, T> : IInterceptionContext<K, T>
    {
        private readonly IAttributeExtender<K> extender;

        public GenericContext(IAttributeExtender<K> attributeExtender)
            => extender = attributeExtender;

        public T this[K key]
        {
            get => (T)extender[key];
            set => extender[key] = value;
        }

        public void Set(K key, T value, bool _override = true)
        {
            if (_override && extender.HasAttribute(key))
                extender[key] = value;
            else
                extender.Add(key, value);
        }

        public bool TryGet(K key, out T t)
            => extender.TryGetAttribute(key, out t);
    }
}
