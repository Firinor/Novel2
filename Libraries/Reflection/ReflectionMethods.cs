using System.Reflection;
using System.Threading.Tasks;

namespace FirReflection
{
    public static class ReflectionMethods
    {
        public static async Task InvokeAsync(MethodInfo method, object obj, params object[] parameters)
        {
            dynamic awaitable = method.Invoke(obj, parameters);
            await awaitable;
        }

        public static async Task<object> InvokeAwaitResult(MethodInfo method, object obj, params object[] parameters)
        {
            dynamic awaitable = method.Invoke(obj, parameters);
            await awaitable;
            return awaitable.GetAwaiter().GetResult();
        }
    }
}
