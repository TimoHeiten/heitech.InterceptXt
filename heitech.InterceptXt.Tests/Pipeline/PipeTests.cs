using heitech.InterceptXt.Interface;
using heitech.InterceptXt.Pipeline;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;

namespace heitech.InterceptXt.Tests.Pipeline
{
    [TestClass]
    public class PipeTests
    {
        private Pipe<string> pipe;
        private readonly string intercept = "interceptString";
        private readonly IntercceptionContextMock fallbackContext = new IntercceptionContextMock();
        private readonly BackwardInterceptorStub<string> backwardIntercept = new BackwardInterceptorStub<string>();
        private readonly InterceptorStub<string> interceptor = new InterceptorStub<string>();

        [TestInitialize]
        public void Init() => pipe = new Pipe<string>(fallbackContext, Do, interceptor, backwardIntercept);

        int countAction = 0;
        private void Do(IIntercept<string> interceptAction)
            => countAction++;

        int countBackwardsAction = 0;
        private void DoBackwards(IIntercept<string> interceptAction)
            => countBackwardsAction++;

        [TestMethod]
        public void Pipe_InterceptWithoutContext_CallsAllInterceptorsWithDefaultContext()
        {
            pipe.Process(intercept);
            Assert.AreSame(fallbackContext, interceptor.Context);
        }

        [TestMethod]
        public void Pipe_InterceptWithContext_DoesNotUseDefaultContext()
        {
            pipe.Process(new IntercceptionContextMock(), intercept);
            Assert.AreNotSame(fallbackContext, interceptor.Context);
        }

        [TestMethod]
        public void Pipe_AnyInterceptInvokes_StartingAction_ForAllInterceptors()
        {
            pipe = new Pipe<string>(fallbackContext, Do, interceptor, interceptor, interceptor);

            InvokeAnyIntercept(() => pipe.Process(intercept));
            InvokeAnyIntercept(() => pipe.Process(fallbackContext, intercept));
            InvokeAnyIntercept(() => pipe.Preprocess(intercept));
            InvokeAnyIntercept(() => pipe.Preprocess(fallbackContext, intercept));
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
            pipe = new Pipe<string>(fallbackContext, Do, DoBackwards, interceptor, backwardIntercept);

            CountBackWardIntercept(() => pipe.Postprocess(intercept));
            CountBackWardIntercept(() => pipe.Postprocess(fallbackContext, intercept));
            CountBackWardIntercept(() => pipe.Process(intercept));
            CountBackWardIntercept(() => pipe.Process(fallbackContext, intercept));
        }

        private void CountBackWardIntercept(Action action)
        {
            action();

            Assert.AreEqual(2, countBackwardsAction);
            countBackwardsAction = 0;
        }

        [TestMethod]
        public void Pipe_ForwardIntercept_InvokesAllItems()
        => ForwardContext(() => pipe.Preprocess(intercept));

        [TestMethod]
        public void Pipe_ForwardWithContext_invokesAllItems()
            => ForwardContext(() => pipe.Preprocess(fallbackContext, intercept));

        private void ForwardContext(Action action)
        {
            action();
            Assert.IsNotNull(interceptor.Context);
            Assert.IsNotNull(backwardIntercept.Context);
        }

        [TestMethod]
        public void Pipe_StartIntercept_InvokesFullCircle_ForwadAndBackwardInvocation()
        {
            pipe = new Pipe<string>(fallbackContext, Do, DoBackwards, interceptor, backwardIntercept);
            pipe.Process(intercept);
            Assert.AreEqual(2, countAction);
            Assert.AreEqual(2, countBackwardsAction);
            Assert.IsNotNull(interceptor.Context);
            Assert.IsNotNull(backwardIntercept.Context);
        }

        [TestMethod]
        public void Pipe_InterceptingBackwardsWithoutContextCallsDefaultContext()
        {
            CheckDefaultUsage(() => pipe.Postprocess(intercept));
            CheckDefaultUsage(() => pipe.Preprocess(intercept));
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
            pipe.Postprocess(intercept);
            Assert.IsTrue(backwardIntercept.WasInvokedDirectly);
        }

        [TestMethod]
        public void Pipe_DoIntercept_InvokesBackwardDirectlyIf_Interceptor_Implements_BothWaysInterface()
        {
            pipe.Process(intercept);
            Assert.IsTrue(backwardIntercept.WasInvokedDirectly);
        }

        [TestMethod]
        public void Pipe_DoIntercept_CallsBackwardsInReverseOrder()
        {
            pipe = new Pipe<string>(fallbackContext, Do, SetFirst, interceptor, backwardIntercept);

            pipe.Postprocess(intercept);
            Assert.AreSame(first, backwardIntercept);
            Assert.AreNotSame(first, interceptor);
        }

        IIntercept<string> first;
        private void SetFirst(IIntercept<string> _first)
        {
            if (first == null)
                first = _first;
        }

    }
}
