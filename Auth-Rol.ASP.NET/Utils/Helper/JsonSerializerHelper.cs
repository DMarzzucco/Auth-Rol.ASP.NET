using System.Text.Json;
using System.Text.Json.Serialization;

namespace Auth_Rol.ASP.NET.Utils.Helper
{
    public static class JsonSerializerHelper
    {
        public static JsonSerializerOptions Default => new()
        {
            ReferenceHandler = ReferenceHandler.IgnoreCycles,
            WriteIndented = true,
            PropertyNamingPolicy = null
        };
    }
}
