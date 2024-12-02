namespace Auth_Rol.ASP.NET.Users.Model
{
    public class UsersModel
    {
        public int Id { get; set; }

        public required string Username { get; set; }

        public required string Email { get; set; }

        public required string Password { get; set; }
    }
}
