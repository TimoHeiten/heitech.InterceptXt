namespace heitech.InterceptXt.Interface
{
    public interface IInterceptionPipe
    {
        void StartIntercept();
        void StartIntercept(IInterceptionContext context);

        void ForwardIntercept();
        void ForwardIntercept(IInterceptionContext context);

        void BackwardIntercept();
        void BackwardIntercept(IInterceptionContext context);
    }
}
