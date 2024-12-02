namespace Auth_Rol.ASP.NET.Users.DTO
{
    public class CreateUserDTO
    {
        public required string Username { get; set; }

        public required string Email { get; set; }

        public required string Password { get; set; }
    }
}
