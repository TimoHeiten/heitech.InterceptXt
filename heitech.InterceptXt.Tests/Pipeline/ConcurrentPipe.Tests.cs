using System.Threading.Tasks;
using heitech.InterceptXt.Interface;
using heitech.InterceptXt.Pipeline;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace heitech.InterceptXt.Tests.Pipeline
{
    [TestClass]
	public class ConcurrentPipeTests
	{
        private readonly ConcurrentInterceptStub intercept_1 = new ConcurrentInterceptStub();
        private readonly ConcurrentInterceptStub intercept_2 = new ConcurrentInterceptStub();
        private readonly ConcurrentInterceptStub intercept_3 = new ConcurrentInterceptStub();
        private readonly IntercceptionContextMock ctxt_mock = new IntercceptionContextMock();

        private ConcurrentInterceptStub[] array;

        private ConcurrentPipe<string> pipe;

        [TestInitialize]
        public void Init()
        {
            array = new ConcurrentInterceptStub[] { intercept_1, intercept_2, intercept_3 };
            pipe = new ConcurrentPipe<string>(array);
        }

        [TestMethod]
        public async Task ConcurrentPipe_Invokes_all_Interceptors_On_Process()
        {
            await pipe.Process("intercept");

            foreach (var item in array)
            {
                Assert.IsTrue(item.WasInvoked);
                Assert.AreEqual("intercept", item.InjectedObj);
            }
        }
	}

    internal class ConcurrentInterceptStub : IConcurrentIntercept<string>
    {
        internal bool WasInvoked { get; private set; }
        internal string InjectedObj;

        public Task InvokeAsync(string obj_toIntercept, IInterceptionContext context)
        {
            WasInvoked = true;
            InjectedObj = obj_toIntercept;
            context.Set<string>("key", "value");

            return Task.CompletedTask;
        }
    }
}
