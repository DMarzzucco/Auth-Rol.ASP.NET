using Auth_Rol.ASP.NET.Users.DTO;
using Auth_Rol.ASP.NET.Users.Model;
using AutoMapper;

namespace Auth_Rol.ASP.NET.Mapper
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<CreateUserDTO, UsersModel>();
            CreateMap<UpdateUserDTO, UsersModel>();
        }
    }
}
