using Auth_Rol.ASP.NET.UserProject.Enum;
using Auth_Rol.ASP.NET.UserProject.Model;
using Auth_Rol.ASP.NET.Utils.Helper;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Auth_Rol.ASP.NET.Context.Configurations
{
    public class UsersProjectModelConfiguration : IEntityTypeConfiguration<UsersProjectModel>
    {
        public void Configure(EntityTypeBuilder<UsersProjectModel> builder)
        {
            builder.HasKey(row => row.Id);
            builder.Property(row => row.Id).UseIdentityColumn().ValueGeneratedOnAdd();

            builder.Property(row => row.AccesLevel)
            .HasConversion(EnumConversionHelper.EnumConversion<AccesLevel>()).IsUnicode(false);

            builder.ToTable("UsersProject");
        }
    }
}
