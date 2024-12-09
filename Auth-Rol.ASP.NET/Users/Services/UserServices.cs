using Auth_Rol.ASP.NET.Exceptions;
using Auth_Rol.ASP.NET.Users.DTO;
using Auth_Rol.ASP.NET.Users.Model;
using Auth_Rol.ASP.NET.Users.Repository.Interface;
using Auth_Rol.ASP.NET.Users.Services.Interface;
using AutoMapper;
using Microsoft.AspNetCore.Identity;

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

        public async Task<UsersModel> CreateUser(CreateUserDTO body)
        {
            if (this._repository.ExistsByUsername(body.Username))
            {
                throw new ConflictException("This username already exists");
            }
            if (this._repository.ExistsByEmail(body.Email))
            {
                throw new ConflictException("This email already exists");
            }
            var data = this._mapper.Map<UsersModel>(body);

            var passwordHasher = new PasswordHasher<UsersModel>();
            data.Password = passwordHasher.HashPassword(data, body.Password);

            await this._repository.AddChangeAsync(data);
            return data;
        }

        public async Task<IEnumerable<UsersModel>> GetAll()
        {
            return await this._repository.ToListAsync();
        }

        public async Task<UsersModel> GetById(int id)
        {
            var person = await this._repository.FindByIdAsync(id);
            if (person == null)
            {
                throw new KeyNotFoundException("User not found");
            }
            return person;
        }

        public async Task<bool> UpdateUser(int id, UpdateUserDTO user)
        {
            var data = await this._repository.FindByIdAsync(id);

            if (data == null)
            {
                throw new KeyNotFoundException("User not found");
            }
            this._mapper.Map(user, data);

            var passwordHasher = new PasswordHasher<UsersModel>();
            data.Password = passwordHasher.HashPassword(data, user.Password);

            await this._repository.Entry(data);

            return true;
        }

        public async Task<bool> DeleteUser(int id)
        {
            var data = await this._repository.FindByIdAsync(id);
            if (data == null)
            {
                throw new KeyNotFoundException("User not found");
            }
            await this._repository.RemoveAsync(data);
            return true;
        }
        public async Task<UsersModel> FindByAuth(string key, object value)
        {
            var user = await this._repository.FindByKey(key, value);
            if (user == null)
            {
                throw new KeyNotFoundException("User not found");
            }
            return user;
        }
    }
}
