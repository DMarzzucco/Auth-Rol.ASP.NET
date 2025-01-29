using Auth_Rol.ASP.NET.Context.Configurations;
using Auth_Rol.ASP.NET.Project.Model;
using Auth_Rol.ASP.NET.UserProject.Model;
using Auth_Rol.ASP.NET.Users.Model;
using Microsoft.EntityFrameworkCore;

namespace Auth_Rol.ASP.NET.Context
{
#pragma warning disable CS1591
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            base.OnConfiguring(optionsBuilder);

            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.EnableSensitiveDataLogging();
            }
        }

        public DbSet<UsersModel> UserModel { get; set; }
        public DbSet<UsersProjectModel> UsersProject { get; set; }
        public DbSet<ProjectModel> ProjectModel { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration(new UsersModelConfiguration());
            modelBuilder.ApplyConfiguration(new UsersProjectModelConfiguration());
            modelBuilder.ApplyConfiguration(new ProjectModelConfiguration());

            base.OnModelCreating(modelBuilder);
        }

    }
#pragma warning restore CS1591

}
