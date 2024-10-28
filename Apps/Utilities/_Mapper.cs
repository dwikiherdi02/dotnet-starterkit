using System.Text.Json;
using Apps.Utilities.Components._Mapper.Extensions;
using AutoMapper;

namespace Apps.Utilities
{
    public sealed class _Mapper
    {
        /**
         ** Mapping object Model, DTO, etc.
         *
         * ? How to use: var todoItem =  _Mapper.Map<TodoRequestBody, TodoItem>(request)
         * 
         * @param TSource source
         * @param ref TDestination destination
         * @result TDestination destination
          */
        public static TDestination Map<TSource, TDestination>(TSource source) 
        where TSource : class
        where TDestination : class
        {
            var configuration = new MapperConfiguration(cfg => {
                cfg.CreateMap<TSource, TDestination>();
            });

            var mapper = configuration.CreateMapper();
            TDestination destination = mapper.Map<TDestination>(source);
            return destination;
        }

        /**
         ** Mapping object Model, DTO, etc.
         *
         * ? How to use: _Mapper.MapTo<TodoRequestBody, TodoItem>(request, ref todoItem)
         * 
         * @param TSource source
         * @param ref TDestination destination
         * @result ref TDestination destination
          */
        public static void MapTo<TSource, TDestination>(TSource source, ref TDestination destination) 
        where TSource : class
        where TDestination : class
        {
            var configuration = new MapperConfiguration(cfg => {
                cfg.CreateMap<TSource, TDestination>().ForAllMembers(opts => opts.Condition((src, dest, srcMember) => srcMember != null));
            });

            var mapper = configuration.CreateMapper();
            
            mapper.Map(source, destination);
        }
    }
}