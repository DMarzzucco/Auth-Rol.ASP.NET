using Microsoft.EntityFrameworkCore;
using User.Context;

namespace User.Configurations.DBConnect.Extensions
{
    public static class AutoMigrationExtensions
    {
        public static void ApplyMigration(this IHost app)
        {
            using var scope = app.Services.CreateScope();
            var dbContext = scope.ServiceProvider.GetRequiredService<AppDBContext>();
            dbContext.Database.Migrate();
        }
    }
}
