using AutoMapper;

namespace EllaMakerTool
{
    public class MapperUtil
    {
        public static void Config(IMapper mapper)
        {
            Mapper = mapper;
        }
        public static IMapper Mapper { get; private set; }

        public static TDestination Map<TDestination>(object source)
        {
            return Mapper.Map<TDestination>(source);
        }

    }
}