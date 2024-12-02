using Auth_Rol.ASP.NET.Users.DTO;
using Auth_Rol.ASP.NET.Users.Model;
using Auth_Rol.ASP.NET.Users.Repository.Interface;
using Auth_Rol.ASP.NET.Users.Services.Interface;
using AutoMapper;

namespace Auth_Rol.ASP.NET.Users.Services
{
    public class UserServices : IUserService
    {

        private readonly IMapper _mapper;
        private readonly IUserRepository _repository;

        public UserServices(IUserRepository repository, IMapper mapper)
        {
            this._mapper = mapper;
            this._repository = repository;
        }

        public Task<UsersModel> CreateUser(CreateUserDTO body)
        {
            throw new NotImplementedException();
        }

        public async Task<IEnumerable<UsersModel>> GetAll()
        {
            return await this._repository.ToListAsync();
        }

        public Task<UsersModel> GetById(int id)
        {
            throw new NotImplementedException();
        }

        public Task<bool> UpdateUser(int id, UpdateUserDTO user)
        {
            throw new NotImplementedException();
        }
        public Task<bool> DeleteUser(int id)
        {
            throw new NotImplementedException();
        }
    }
}
