using heitech.InterceptXt.Interface;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;

namespace heitech.InterceptXt.Tests.Interface
{
    [TestClass]
    public class FactoryTests
    {
        [TestMethod]
        public void Factory_Create_ThrowsNotImplementedException()
        {
            Assert.ThrowsException<NotImplementedException>(() => Factory.Create());
        }
    }
}
