using User.Utils.Filter;
using System.Text.Json.Serialization;

namespace User.Configurations
{
    public static class ServiceControllerBuilder
    {
        public static IServiceCollection AddServiceControllerBuilder(this IServiceCollection service)
        {

            service.AddControllers(static e =>
            {
                e.Filters.Add(typeof(GlobalFilterExceptions));

            }).AddJsonOptions(op =>
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
