

namespace Apps.Middlewares.Attributes
{
    [AttributeUsage(AttributeTargets.Method | AttributeTargets.Class, AllowMultiple = false)]
    public class AuthMiddlewareAttribute : Attribute
    {
        
    }
}