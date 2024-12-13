using Auth_Rol.ASP.NET.Project.Model;
using Auth_Rol.ASP.NET.Users.Enums;

namespace Auth_Rol.ASP.NET.Users.Model
{
    public class UsersProject
    {
        public required AccesLevel AccesLevel { get; set; }

        public required UsersModel User { get; set; }

        public required ProjectModel Project { get; set; }
    }
}
