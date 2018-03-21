using heitech.InterceptXt.Interface;
using heitech.InterceptXt.Pipeline;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;

namespace heitech.InterceptXt.Tests.Interface
{
    [TestClass]
    public class FactoryTests
    {
        [TestMethod]
        public void Factory_Create_Returns_Pipe_WithDefaultInterceptionContext_IfNotSpecified()
        {
            Action<IIntercept> _do = _ => { };
            Action<IIntercept> _doBack = _ => { };
            var interceptors = new IIntercept[] { };
            Do(() => Factory.Create(interceptors));
            Do(() => Factory.Create(_do, interceptors));
            Do(() => Factory.Create(_do, _doBack, interceptors));
            Do(() => Factory.Create(_do, _doBack, new IntercceptionContextMock(), interceptors));
        }

        private void Do(Func<IInterceptionPipe> _do)
        {
            IInterceptionPipe pipe = _do();
            Assert.AreEqual(typeof(Pipe), pipe.GetType());
        }

        [TestMethod]
        public void Factory_CreateMultiPipe_Returns_MultiPipe()
        {
            var multi = Factory.CreateMultiPipe<string>();
            Assert.AreEqual(typeof(MultiPipe<string>), multi.GetType());
        }
    }
}
