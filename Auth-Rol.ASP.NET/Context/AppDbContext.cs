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

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
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


            });

            modelBuilder.Entity<UsersModel>().ToTable("Users");
        }
    }
#pragma warning restore CS1591

}
