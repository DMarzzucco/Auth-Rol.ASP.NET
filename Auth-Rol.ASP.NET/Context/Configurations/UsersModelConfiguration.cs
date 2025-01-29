using Auth_Rol.ASP.NET.Users.Enums;
using Auth_Rol.ASP.NET.Users.Model;
using Auth_Rol.ASP.NET.Utils.Helper;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Auth_Rol.ASP.NET.Context.Configurations
{
    public class UsersModelConfiguration : IEntityTypeConfiguration<UsersModel>
    {
        public void Configure(EntityTypeBuilder<UsersModel> builder)
        {
            builder.HasKey(row => row.Id);
            builder.Property(row => row.Id).UseIdentityColumn().ValueGeneratedOnAdd();

            builder.Property(row => row.First_name).HasMaxLength(100);
            builder.Property(row => row.Last_name).HasMaxLength(100);
            builder.Property(row => row.Age).HasMaxLength(100);

            builder.Property(row => row.Username).HasMaxLength(50).IsUnicode();
            builder.Property(row => row.Email).HasMaxLength(50).IsUnicode();
            builder.Property(row => row.Password);

            builder.Property(row => row.Roles)
            .HasConversion(EnumConversionHelper.EnumConversion<ROLES>()).HasMaxLength(20).IsUnicode(false);

            builder.Property(row => row.RefreshToken).IsRequired(false);

            builder.HasMany(e => e.ProjectsIncludes)
            .WithOne(p => p.User)
            .HasForeignKey(p => p.UserId)
            .OnDelete(DeleteBehavior.Cascade);

            builder.ToTable("Users");
        }
    }
}
