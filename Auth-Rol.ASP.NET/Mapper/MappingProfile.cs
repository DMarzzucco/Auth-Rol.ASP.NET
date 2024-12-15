using Auth_Rol.ASP.NET.Users.DTO;
using Auth_Rol.ASP.NET.Users.Model;
using AutoMapper;

namespace Auth_Rol.ASP.NET.Mapper
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            //Create new user
            CreateMap<CreateUserDTO, UsersModel>();

            //Update new User
            CreateMap<UpdateUserDTO, UsersModel>();

            //Realtionation between User and Project
            CreateMap<UsersProjectDTO, UsersProjectModel>();
        }
    }
}
