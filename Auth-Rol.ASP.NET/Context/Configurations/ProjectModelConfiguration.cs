using Auth_Rol.ASP.NET.Project.Model;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Auth_Rol.ASP.NET.Context.Configurations
{
    public class ProjectModelConfiguration : IEntityTypeConfiguration<ProjectModel>
    {
        public void Configure(EntityTypeBuilder<ProjectModel> builder)
        {
            builder.HasKey(row => row.Id);
            builder.Property(row => row.Id).UseIdentityColumn().ValueGeneratedOnAdd();
            builder.Property(row => row.Name);
            builder.Property(row => row.Description);

            builder.HasMany(e => e.UsersIncludes)
            .WithOne(p => p.Project)
            .HasForeignKey(p => p.ProjectId).OnDelete(DeleteBehavior.Cascade);

            builder.ToTable("Projects");
        }
    }
}
