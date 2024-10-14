using AutoMapper;

namespace Apps.Utilities
{
    public class _Mapper
    {
        /**
         ** Mapping object Model, DTO, etc.
         *
         * ? How to use: Util.Mapper<TodoRequestBody, TodoItem>(request, ref todoItem)
         * 
         * @param TSource source
         * @param ref TDestination destination
         * @result TDestination destination
          */
        public static void Map<TSource, TDestination>(TSource source, ref TDestination destination) 
        where TSource : class
        where TDestination : class
        {
            var configuration = new MapperConfiguration(cfg => {
                cfg.CreateMap<TSource, TDestination>();
            });

            var mapper = configuration.CreateMapper();
            destination = mapper.Map<TDestination>(source);
        }

        public static TDestination MapTo<TSource, TDestination>(TSource source) 
        // where TSource : class
        // where TDestination : class
        {
            var configuration = new MapperConfiguration(cfg => {
                cfg.CreateMap<TSource, TDestination>();
            });

            var mapper = configuration.CreateMapper();
            TDestination destination = mapper.Map<TDestination>(source);
            return destination;
        }
    }
}