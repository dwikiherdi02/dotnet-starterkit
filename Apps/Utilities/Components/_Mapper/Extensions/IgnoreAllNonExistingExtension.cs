using System.Reflection;
using AutoMapper;

namespace Apps.Utilities.Components._Mapper.Extensions
{
    public static class IgnoreAllNonExistingExtension
    {
        // public static IMappingExpression<TSource, TDestination> IgnoreAllNonExisting<TSource, TDestination>(this IMappingExpression<TSource, TDestination> expression)
        // {
        //     var sourceType = typeof(TSource);
        //     var destinationType = typeof(TDestination);
        //     var existingMaps = Mapper.GetAllTypeMaps().First(x => x.SourceType.Equals(sourceType) && x.DestinationType.Equals(destinationType));
        //     foreach (var property in existingMaps.GetUnmappedPropertyNames())
        //     {
        //         expression.ForMember(property, opt => opt.Ignore());
        //     }
        //     return expression;
        // }

        public static IMappingExpression<TSource, TDestination> IgnoreAllNonExisting<TSource, TDestination>
        (this IMappingExpression<TSource, TDestination> expression)
        {
            var flags = BindingFlags.Public | BindingFlags.Instance;
            var sourceType = typeof (TSource);
            var destinationProperties = typeof (TDestination).GetProperties(flags);

            foreach (var property in destinationProperties)
            {
                if (sourceType.GetProperty(property.Name, flags) == null)
                {
                    expression.ForMember(property.Name, opt => opt.Ignore());
                }
            }
            return expression;
        }
    }
}