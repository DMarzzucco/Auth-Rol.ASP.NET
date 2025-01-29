using Auth_Rol.ASP.NET.Context;
using Microsoft.EntityFrameworkCore;

namespace Auth_Rol.ASP.NET.Configuration.DbConfiguration.Extensions
{
    public static class AutoMigrationExtension
    {
        public static void ApplyMigration(this IHost app)
        {
            using var scope = app.Services.CreateScope();
            var dbContext = scope.ServiceProvider.GetRequiredService<AppDbContext>();
            dbContext.Database.Migrate();
        }
    }
}
