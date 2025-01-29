using Auth_Rol.ASP.NET.Utils.Filter;
using System.Text.Json.Serialization;

namespace Auth_Rol.ASP.NET.Configuration
{
    public static class ControllerExtension
    {
        public static IServiceCollection AddCustomController(this IServiceCollection service)
        {
            service.AddControllers(op =>
            {
                op.Filters.Add<GlobalFilterExceptions>();
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
