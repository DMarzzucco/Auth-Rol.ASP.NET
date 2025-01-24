namespace Auth_Rol.ASP.NET.Auth.JWT.DTOs
{
    public class TokenPair
    {
        public required string AccessToken { get; set; }
        public required string RefreshToken { get; set; }
        public required string RefreshTokenHashed { get; set; }
    }
}
