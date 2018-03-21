using heitech.InterceptXt.Pipeline;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;

namespace heitech.InterceptXt.Tests.Pipeline
{
    [TestClass]
    public class MultiPipeTests
    {
        private readonly MultiPipe<string> pipe = new MultiPipe<string>();
        private readonly InterceptionPipeMock mock = new InterceptionPipeMock();

        [TestMethod]
        public void Multipipe_MapOverridesItem_OnMapTwice()
        {
            pipe.Map("key", mock);
            pipe.Map("key", new InterceptionPipeMock());

            Assert.AreNotSame(mock, pipe["key"]);
        }

        [TestMethod]
        public void Multipipe_AnyIntercept_DelegatesTo_MappedPipe_ofThatKey()
        {
            string key = "key";
            pipe.Map(key, mock);
            AssertMap(() => pipe.StartIntercept(key));
            AssertMap(() => pipe.ForwardIntercept(key));
            AssertMap(() => pipe.BackwardIntercept(key));
        }

        private void AssertMap(Action _do)
        {
            _do();
            Assert.IsTrue(mock.WasInvoked);
            mock.WasInvoked = false;
        }

        [TestMethod]
        public void Multipipe_ThrowsAttributeNotFoundException_ifPipe_IsNotMapped()
        {
            Assert.ThrowsException<KeyNotFoundException>(() => pipe.StartIntercept("key"));
            Assert.ThrowsException<KeyNotFoundException>(() => pipe.ForwardIntercept("key"));
            Assert.ThrowsException<KeyNotFoundException>(() => pipe.BackwardIntercept("key"));
        }
    }
}
