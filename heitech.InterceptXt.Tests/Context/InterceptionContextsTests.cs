using heitech.InterceptXt.Context;
using heitech.ObjectXt.AttributeExtension;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace heitech.InterceptXt.Tests.Context
{
    [TestClass]
    public class InterceptionContextsTests
    {
        private InterceptorContext context;

        [TestInitialize]
        public void Init()
        {
            var extender = AttributeExtenderFactory.Create<string>();
            context = new InterceptorContext(extender);
        }

        [TestMethod]
        public void InterceptionContext_IndexerGetsValue()
        {

        }
    }
}
