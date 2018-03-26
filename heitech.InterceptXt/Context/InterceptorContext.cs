using heitech.InterceptXt.Interface;
using heitech.ObjectXt.Interfaces;
using System.Diagnostics.CodeAnalysis;

namespace heitech.InterceptXt.Context
{
    // only a wrapper around AttributeExtender
    [ExcludeFromCodeCoverage]
    public class InterceptorContext : IInterceptionContext
    {
        private readonly IAttributeExtender<string> extender;

        public InterceptorContext(IAttributeExtender<string> extender) => this.extender = extender;

        public object this[string val]
        {
            get => extender[val];
            set => extender[val] = value;
        }

        public void Set<T>(string key, object value, bool _override = true)
        {
            if (_override && extender.HasAttribute(key))
                extender[key] = value;
            else
                extender.Add(key, value);
        }

        public bool TryGet<T>(string key, out T t) 
            => extender.TryGetAttribute<T>(key, out t);
    }
}
