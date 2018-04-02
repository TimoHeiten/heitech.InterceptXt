using System.Threading.Tasks;
namespace heitech.InterceptXt.Interface
{
    public interface IConcurrentIntercept<T>
    {
        Task InvokeAsync(T obj_toIntercept, IInterceptionContext context);
    }
}