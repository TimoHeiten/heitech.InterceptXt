using heitech.InterceptXt.Interface;
using System;
using System.Collections.Generic;
using System.Text;

namespace heitech.InterceptXt.Tests
{
    internal class IntercceptionContextMock : IInterceptionContext
    {
        internal object Value;
        internal Dictionary<string, object> dictionary = new Dictionary<string, object>();

        internal bool CanGetValue;

        public object this[string key]
        {
            get => dictionary[key];
            set => dictionary.Add(key, value);
        }

        public void Set<T>(string key, object value, bool _override = true)
        {
            dictionary[key] = value;
        }

        public bool TryGet<T>(string key, out T t)
        {
            t = (T)Value;
            return CanGetValue;
        }
    }
}
