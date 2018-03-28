using heitech.InterceptXt.Pipeline;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;

namespace heitech.InterceptXt.Tests.Pipeline
{
    [TestClass]
    public class MultiPipeTests
    {
        private readonly MultiPipe<string, string> pipe = new MultiPipe<string, string>();
        private readonly InterceptionPipeMock<string> mock = new InterceptionPipeMock<string>();
        private readonly string intercept = "empty";

        [TestMethod]
        public void Multipipe_MapOverridesItem_OnMapTwice()
        {
            pipe.Map("key", mock);
            pipe.Map("key", new InterceptionPipeMock<string>());

            Assert.AreNotSame(mock, pipe["key"]);
        }

        [TestMethod]
        public void Multipipe_AnyIntercept_DelegatesTo_MappedPipe_ofThatKey()
        {
            string key = "key";
            pipe.Map(key, mock);
            AssertMap(() => pipe.Process(key, intercept));
            AssertMap(() => pipe.Preprocess(key, intercept));
            AssertMap(() => pipe.Postprocess(key, intercept));
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
            Assert.ThrowsException<KeyNotFoundException>(() => pipe.Process("key", intercept));
            Assert.ThrowsException<KeyNotFoundException>(() => pipe.Preprocess("key", intercept));
            Assert.ThrowsException<KeyNotFoundException>(() => pipe.Postprocess("key", intercept));
        }
    }
}
