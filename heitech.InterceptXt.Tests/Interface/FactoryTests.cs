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
            Action<IIntercept<int>> _do = _ => { };
            Action<IIntercept<int>> _doBack = _ => { };
            var interceptors = new IIntercept<int>[] { };
            Do(() => Factory.Create(interceptors));
            Do(() => Factory.Create(_do, interceptors));
            Do(() => Factory.Create(_do, _doBack, interceptors));
            Do(() => Factory.Create(_do, _doBack, new IntercceptionContextMock(), interceptors));
        }

        private void Do(Func<IInterceptionPipe<int>> _do)
        {
            IInterceptionPipe<int> pipe = _do();
            Assert.AreEqual(typeof(Pipe<int>), pipe.GetType());
        }

        [TestMethod]
        public void Factory_CreateMultiPipe_Returns_MultiPipe()
        {
            var multi = Factory.CreateMultiPipe<string, int>();
            Assert.AreEqual(typeof(MultiPipe<string, int>), multi.GetType());
        }
    }
}
