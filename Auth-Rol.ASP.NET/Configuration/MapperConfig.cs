using Auth_Rol.ASP.NET.Mapper;
using AutoMapper;

namespace Auth_Rol.ASP.NET.Configuration
{
    public static class MapperConfig
    {
        public static IServiceCollection AddMapperConfig(this IServiceCollection services)
        {
            var mappConfig = new MapperConfiguration(cfg =>
            {
                cfg.AddProfile<MappingProfile>();
            });
            IMapper mapper = mappConfig.CreateMapper();

            services.AddSingleton(mapper);

            return services;
        }
    }
}
