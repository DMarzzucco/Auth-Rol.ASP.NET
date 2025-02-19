using AutoMapper;
using User.Module.DTOs;
using User.Module.Model;

namespace User.Mapper
{
    public class MappingProfile:Profile
    {
        public MappingProfile()
        {
            CreateMap<CreateUserDTO, UserModel>();
            CreateMap<UpdateUserDTO, UserModel>();
        }
    }
}
