using Auth_Rol.ASP.NET.Users.Model;

namespace Auth_Rol.ASP.NET.Project.Model
{
    public class ProjectModel
    {
        public required int Id { get; set; }
        public required string Name { get; set; }
        public required string Description { get; set; }
        public required UsersProject UserProject { get; set; }
    }
}
