using Auth_Rol.ASP.NET.Project.Model;
using Auth_Rol.ASP.NET.Users.Enums;
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

        public DbSet<UsersModel> UserModel { get; set; }
        public DbSet<UsersProjectModel> UsersProject { get; set; }
        public DbSet<ProjectModel> ProjectModel { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            //Users Model Conf
            modelBuilder.Entity<UsersModel>(tb =>
            {
                tb.HasKey(row => row.Id);
                tb.Property(row => row.Id).UseIdentityColumn().ValueGeneratedOnAdd();

                tb.Property(row => row.First_name).HasMaxLength(100);
                tb.Property(row => row.Last_name).HasMaxLength(100);
                tb.Property(row => row.Age).HasMaxLength(100);

                tb.Property(row => row.Username).HasMaxLength(50).IsUnicode();
                tb.Property(row => row.Email).HasMaxLength(50).IsUnicode();
                tb.Property(row => row.Password);

                tb.Property(row => row.Roles)
                .HasConversion(
                    v => v.ToString(),
                    v => (ROLES)Enum.Parse(typeof(ROLES), v)
                    ).HasMaxLength(20).IsUnicode(false);

                tb.Property(row => row.RefreshToken).IsRequired(false);

                tb.HasMany(e => e.ProjectsIncludes)
                .WithOne(p => p.User)
                .HasForeignKey(p => p.UserId)
                .OnDelete(DeleteBehavior.Cascade);
            });

            modelBuilder.Entity<UsersModel>().ToTable("Users");

            //UsersProject Model Conf
            modelBuilder.Entity<UsersProjectModel>(tb =>
            {
                tb.HasKey(row => row.Id);
                tb.Property(row => row.AccesLevel)
                .HasConversion(
                    v => v.ToString(),
                    v => (AccesLevel)Enum.Parse(typeof(AccesLevel), v)
                    ).IsUnicode(false);
            });
            modelBuilder.Entity<UsersProjectModel>().ToTable("UsersProject");

            //Project Model Conf
            modelBuilder.Entity<ProjectModel>(tb =>
            {
                tb.HasKey(row => row.Id);
                tb.Property(row => row.Name);
                tb.Property(row => row.Description);

                tb.HasMany(e => e.UsersIncludes)
                .WithOne(p => p.Project)
                .HasForeignKey(p => p.ProjectId).OnDelete(DeleteBehavior.Cascade);
            });
            modelBuilder.Entity<ProjectModel>().ToTable("Projects");

        }
    }
#pragma warning restore CS1591

}
