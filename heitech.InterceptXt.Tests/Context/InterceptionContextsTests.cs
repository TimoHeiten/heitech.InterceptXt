using heitech.InterceptXt.Context;
using heitech.ObjectXt.AttributeExtension;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace heitech.InterceptXt.Tests.Context
{
    [TestClass]
    public class InterceptionContextsTests
    {
        private InterceptorContext context;
        private GenericContext<string, int> generic;

        [TestInitialize]
        public void Init()
        {
            var extender = AttributeExtenderFactory.Create<string>();
            context = new InterceptorContext(extender);
            generic = new GenericContext<string, int>(extender);
        }

        [TestMethod]
        public void InterceptionContext_IndexerGetsValue()
        {

        }
    }
}
