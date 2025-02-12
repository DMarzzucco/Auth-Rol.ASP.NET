using AutoMapper;
using User.DTOs;
using User.Model;

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
