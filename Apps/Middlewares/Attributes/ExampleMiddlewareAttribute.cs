
namespace Apps.Middlewares.Attributes
{
    [AttributeUsage(AttributeTargets.Method | AttributeTargets.Class, AllowMultiple = false)]
    public class ExampleMiddlewareAttribute : Attribute
    {
        public string Name { get; set; } = string.Empty;

        public ExampleMiddlewareAttribute(string name)
        {
            Name = name;
        }
    }
}