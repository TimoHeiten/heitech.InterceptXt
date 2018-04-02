using System;
using System.Threading.Tasks;
namespace heitech.InterceptXt.Interface
{
    public interface IConcurrentInterceptionPipe<T>
    {
        Task Process(T obj_to_intercept);
        Task Process(IInterceptionContext context, T obj_to_intercept);
    }
}
