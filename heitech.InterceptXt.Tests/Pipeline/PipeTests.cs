using heitech.InterceptXt.Interface;
using heitech.InterceptXt.Pipeline;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;

namespace heitech.InterceptXt.Tests.Pipeline
{
    [TestClass]
    public class PipeTests
    {
        private Pipe pipe;

        private readonly IntercceptionContextMock fallbackContext = new IntercceptionContextMock();

        private readonly BackwardInterceptorStub backwardIntercept = new BackwardInterceptorStub();

        private readonly InterceptorStub interceptor = new InterceptorStub();

        [TestInitialize]
        public void Init() => pipe = new Pipe(fallbackContext, Do, interceptor, backwardIntercept);

        int countAction = 0;
        private void Do(IIntercept intercept)
            => countAction++;

        int countBackwardsAction = 0;
        private void DoBackwards(IIntercept intercept)
            => countBackwardsAction++;

        [TestMethod]
        public void Pipe_InterceptWithoutContext_CallsAllInterceptorsWithDefaultContext()
        {
            pipe.StartIntercept();
            Assert.AreSame(fallbackContext, interceptor.Context);
        }

        [TestMethod]
        public void Pipe_InterceptWithContext_DoesNotUseDefaultContext()
        {
            pipe.StartIntercept(new IntercceptionContextMock());
            Assert.AreNotSame(fallbackContext, interceptor.Context);
        }

        [TestMethod]
        public void Pipe_AnyInterceptInvokes_StartingAction_ForAllInterceptors()
        {
            pipe = new Pipe(fallbackContext, Do, interceptor, interceptor, interceptor);

            InvokeAnyIntercept(pipe.StartIntercept);
            InvokeAnyIntercept(() => pipe.StartIntercept(fallbackContext));
            InvokeAnyIntercept(pipe.ForwardIntercept);
            InvokeAnyIntercept(() => pipe.ForwardIntercept(fallbackContext));
        }

        private void InvokeAnyIntercept(Action action)
        {
            action();
            Assert.AreEqual(3, countAction);
            countAction = 0;
        }

        [TestMethod]
        public void Pipe_BackwardsIntercept_InvokeBackwardsInterceptAndAction_ForeachInterceptor()
        {
            pipe = new Pipe(fallbackContext, Do, DoBackwards, interceptor, backwardIntercept);

            CountBackWardIntercept(pipe.BackwardIntercept);
            CountBackWardIntercept(() => pipe.BackwardIntercept(fallbackContext));
            CountBackWardIntercept(pipe.StartIntercept);
            CountBackWardIntercept(() => pipe.StartIntercept(fallbackContext));
        }

        private void CountBackWardIntercept(Action action)
        {
            action();

            Assert.AreEqual(2, countBackwardsAction);
            countBackwardsAction = 0;
        }

        [TestMethod]
        public void Pipe_ForwardIntercept_InvokesAllItems()
            => ForwardContext(pipe.ForwardIntercept);

        [TestMethod]
        public void Pipe_ForwardWithContext_invokesAllItems()
            => ForwardContext(() => pipe.ForwardIntercept(fallbackContext));

        private void ForwardContext(Action action)
        {
            action();
            Assert.IsNotNull(interceptor.Context);
            Assert.IsNotNull(backwardIntercept.Context);
        }

        [TestMethod]
        public void Pipe_StartIntercept_InvokesFullCircle_ForwadAndBackwardInvocation()
        {
            pipe = new Pipe(fallbackContext, Do, DoBackwards, interceptor, backwardIntercept);
            pipe.StartIntercept();
            Assert.AreEqual(2, countAction);
            Assert.AreEqual(2, countBackwardsAction);
            Assert.IsNotNull(interceptor.Context);
            Assert.IsNotNull(backwardIntercept.Context);
        }

        [TestMethod]
        public void Pipe_InterceptingBackwardsWithoutContextCallsDefaultContext()
        {
            CheckDefaultUsage(pipe.BackwardIntercept);
            CheckDefaultUsage(pipe.ForwardIntercept);
        }

        private void CheckDefaultUsage(Action action)
        {
            action();
            Assert.AreSame(fallbackContext, interceptor.Context);
            Assert.AreSame(fallbackContext, backwardIntercept.Context);
        }

        [TestMethod]
        public void Pipe_InvokesBackwardInvoke_IfInterceptorImplementsInterfaceIBothways_Directly()
        {
            pipe.BackwardIntercept();
            Assert.IsTrue(backwardIntercept.WasInvokedDirectly);
        }

        [TestMethod]
        public void Pipe_DoIntercept_InvokesBackwardDirectlyIf_Interceptor_Implements_BothWaysInterface()
        {
            pipe.StartIntercept();
            Assert.IsTrue(backwardIntercept.WasInvokedDirectly);
        }

        [TestMethod]
        public void Pipe_DoIntercept_CallsBackwardsInReverseOrder()
        {
            pipe = new Pipe(fallbackContext, Do, SetFirst, interceptor, backwardIntercept);

            pipe.BackwardIntercept();
            Assert.AreSame(first, backwardIntercept);
            Assert.AreNotSame(first, interceptor);
        }

        IIntercept first;
        private void SetFirst(IIntercept intercept)
        {
            if (first == null)
                first = intercept;
        }

    }
}
