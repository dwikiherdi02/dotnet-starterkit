using System.Reflection;
using FluentValidation.Results;
using Microsoft.AspNetCore.Mvc;

namespace Apps.Utilities._ValidationErrorBuilder
{
    public sealed class _ValidationErrorBuilder
    {
        public static Dictionary<string, string> Generate<T>(List<ValidationFailure>? errors) where T : class
        {
            var dict = new Dictionary<string, string>();

            if (errors != null)
            {
                foreach(var failure in errors)
                {   
                    string propertyName = _Convertion._Property.ToQueryPropName<T>(failure.PropertyName);
                    dict.Add(propertyName, failure.ErrorMessage);
                }
            }

            return dict;
        }
    }
}