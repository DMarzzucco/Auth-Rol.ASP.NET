using Ocelot.DependencyInjection;

namespace Gateway.Configurations.Ocelot
{
    public static class OcelotConfiguration
    {
        public static IServiceCollection AddOcelotConfiguration(this IServiceCollection service) {

            service.AddOcelot();


            return service;
        }
    }
}
