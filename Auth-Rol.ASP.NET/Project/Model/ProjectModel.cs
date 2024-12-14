using Auth_Rol.ASP.NET.Filter.Attributes;
using Auth_Rol.ASP.NET.Users.Model;
using Swashbuckle.AspNetCore.Annotations;

namespace Auth_Rol.ASP.NET.Project.Model
{
    public class ProjectModel
    {
        [SwaggerSchema("Project Id ")]
        public required int Id { get; set; }

        [SwaggerSchema("Project Name")]
        [SwaggerSchemaExample("Next year's projects.")]
        public required string Name { get; set; }

        [SwaggerSchema("Project Description")]
        [SwaggerSchemaExample("Next year's projects include basic tasks for beginners, as well as a review of errors made the previous year.")]
        public required string Description { get; set; }

        [SwaggerIgnore]
        public ICollection<UsersProjectModel> UsersIncludes { get; set; } = new List<UsersProjectModel>();
    }
}
