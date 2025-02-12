using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using User.Enums;
using User.Model;
using User.Utils.Helper;

namespace User.Context.Configuration
{
    public class UserModelConfiguration : IEntityTypeConfiguration<UserModel>
    {
        public void Configure(EntityTypeBuilder<UserModel> builder)
        {
            builder.HasKey(r => r.Id);
            builder.Property(r => r.Id).UseIdentityColumn().ValueGeneratedOnAdd();

            builder.Property(r => r.First_name).HasMaxLength(100);
            builder.Property(r => r.Last_name).HasMaxLength(100);
            builder.Property(r => r.Age).HasMaxLength(100);

            builder.Property(r => r.Username).HasMaxLength(50).IsUnicode();
            builder.Property(row => row.Email).HasMaxLength(50).IsUnicode();
            builder.Property(row => row.Password);

            builder.Property(row => row.Roles)
            .HasConversion(EnumConversionHelper.EnumConversion<ROLES>()).HasMaxLength(20).IsUnicode(false);

            builder.Property(row => row.RefreshToken).IsRequired(false);

            builder.ToTable("Users");
        }
    }
}
