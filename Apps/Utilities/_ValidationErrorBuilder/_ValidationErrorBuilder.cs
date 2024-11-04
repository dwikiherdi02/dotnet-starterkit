using System.Reflection;
using FluentValidation.Results;
using Microsoft.AspNetCore.Mvc;

namespace Apps.Utilities._ValidationErrorBuilder
{
    public sealed class _ValidationErrorBuilder
    {
        public static Dictionary<string, string> Generate<T>(List<ValidationFailure>? errors, string entityType = "query") where T : class
        {
            var dict = new Dictionary<string, string>();

            if (errors != null)
            {
                foreach(var failure in errors)
                {   
                    string propertyName = failure.PropertyName;
                    switch (entityType)
                    {
                        case "query":
                            propertyName = _Convertion._Property.ToQueryPropName<T>(propertyName);
                            break;

                        case "json":
                            propertyName = _Convertion._Property.ToJsonPropName<T>(propertyName);
                            break;
                    }
                    // string propertyName = _Convertion._Property.ToQueryPropName<T>(failure.PropertyName);
                    dict.Add(propertyName, failure.ErrorMessage);
                }
            }

            return dict;
        }
    }
}