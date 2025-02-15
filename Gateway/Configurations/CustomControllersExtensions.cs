using Gateway.Utils.Filter;
using System.Text.Json.Serialization;

namespace Gateway.Configurations
{
    public static class CustomControllersExtensions
    {
        public static IServiceCollection AddCustomController(this IServiceCollection service)
        {

            service.AddControllers(static e =>
            {
                e.Filters.Add(typeof(GlobalFilterExeption));
            }
                ).AddJsonOptions(op =>
                {
                    op.JsonSerializerOptions.PropertyNamingPolicy = null;
                    op.JsonSerializerOptions.WriteIndented = true;
                    op.JsonSerializerOptions.DefaultIgnoreCondition = JsonIgnoreCondition.WhenWritingNull;
                    op.JsonSerializerOptions.ReferenceHandler = ReferenceHandler.IgnoreCycles;
                });
            return service;
        }
    }
}
