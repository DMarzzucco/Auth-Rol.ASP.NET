using Auth_Rol.ASP.NET.Project.DTO;
using Auth_Rol.ASP.NET.Project.Model;
using Auth_Rol.ASP.NET.UserProject.DTOs;
using Auth_Rol.ASP.NET.UserProject.Model;
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

            //Update User
            CreateMap<UpdateUserDTO, UsersModel>();

            //Realtionation between User and Project
            CreateMap<UsersProjectDTO, UsersProjectModel>();


            //Create new Project
            CreateMap<CreateProjectDTO, ProjectModel>();

            //Update Project
            CreateMap<UpdateProjectDTO, ProjectModel>();
        }
    }
}
