using System.Reflection;
using System.Text.Json.Serialization;
using Microsoft.AspNetCore.Mvc;

namespace Apps.Utilities._Convertion
{
    public class _Property
    {
        public static string ToQueryPropName<T>(string propertyName) where T : class
        {
            string queryName = propertyName;

            Type type = typeof(T);

            PropertyInfo? property = type.GetProperty(propertyName);

            if (property != null)
            {
                var fromQueryAttr = property.GetCustomAttribute<FromQueryAttribute>();

                queryName = fromQueryAttr?.Name ?? propertyName;
            }

            return queryName;
        }

        public static string ToJsonPropName<T>(string propertyName) where T : class
        {
            var jsonName = propertyName;

            var type = typeof(T);

            PropertyInfo? property = type.GetProperty(propertyName);

            if (property != null)
            {
                var fromJsonAttr = property.GetCustomAttribute<JsonPropertyNameAttribute>();

                jsonName = fromJsonAttr?.Name ?? propertyName;
            }

            return jsonName;
        }

        /* public static string ToBodyPropName<T>(string propertyName) where T : class
        {
            string bodyName = propertyName;

            Type type = typeof(T);

            // PropertyInfo? property = type.GetProperty(propertyName);

            foreach (var method in type.GetMethods())
            {
                foreach (var parameter in method.GetParameters())
                {
                    if (parameter.Name == propertyName)
                    {
                        var attribute = parameter.GetCustomAttribute<FromBodyAttribute>();
                        
                        if (attribute != null)
                        {
                            bodyName = method.Name ?? propertyName;
                        }
                    }
                }
            }

            return bodyName;
        } */
    }
}