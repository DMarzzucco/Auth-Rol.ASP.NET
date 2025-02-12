using AutoMapper;
using User.Mapper;

namespace User.Configurations
{
    public static class AutoMapperConfiguration
    {
        public static IServiceCollection AddAutoMapperConfig(this IServiceCollection service) {

            var mappConfig = new MapperConfiguration(conf => {

                conf.AddProfile<MappingProfile>();
            });

            IMapper mapper = mappConfig.CreateMapper();

            service.AddSingleton(mapper);

            return service;
        }
    }
}
