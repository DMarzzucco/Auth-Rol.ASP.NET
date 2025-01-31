using Auth_Rol.ASP.NET.UserProject.DTOs;
using Auth_Rol.ASP.NET.UserProject.Model;
using Auth_Rol.ASP.NET.UserProject.Repository.Interface;
using Auth_Rol.ASP.NET.Users.DTO;
using Auth_Rol.ASP.NET.Users.Model;
using Auth_Rol.ASP.NET.Users.Repository.Interface;
using Auth_Rol.ASP.NET.Users.Services.Interface;
using Auth_Rol.ASP.NET.Utils.Exceptions;
using AutoMapper;
using Microsoft.AspNetCore.Identity;

namespace Auth_Rol.ASP.NET.Users.Services
{
    public class UserServices : IUserService
    {

        private readonly IMapper _mapper;
        private readonly IUserRepository _repository;
        private readonly IUserProjectRepository _userProjectRepository;

        public UserServices(IUserRepository repository, IMapper mapper, IUserProjectRepository userProjectRepository)
        {
            this._mapper = mapper;
            this._repository = repository;
            this._userProjectRepository = userProjectRepository;
        }

        public async Task<UsersModel> CreateUser(CreateUserDTO body)
        {
            if (this._repository.ExistsByUsername(body.Username)) throw new ConflictException("This Username already exists");

            if (this._repository.ExistsByEmail(body.Email)) throw new ConflictException("This email already exists");

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
            if (person == null) throw new KeyNotFoundException("User not found");
            return person;
        }

        public async Task<UsersModel> UpdateUser(int id, UpdateUserDTO user)
        {
            var data = await GetById(id);

            this._mapper.Map(user, data);

            //var passwordHasher = new PasswordHasher<UsersModel>();
            //data.Password = passwordHasher.HashPassword(data, user.Password);

            await this._repository.UpdateAsync(data);

            return data;
        }

        public async Task DeleteUser(int id)
        {
            var data = await GetById(id);
            await this._repository.RemoveAsync(data);
        }

        //Fin by key and value
        public async Task<UsersModel> FindByAuth(string key, object value)
        {
            var user = await this._repository.FindByKey(key, value);
            if (user == null) throw new KeyNotFoundException("User not found");

            return user;
        }

        //Refresh Token Update
        public async Task<UsersModel> updateToken(int id, string RefreshToken)
        {
            var user = await GetById(id);
            user.RefreshToken = RefreshToken;
            await this._repository.UpdateAsync(user);
            return user;
        }

        //relation Project
        public async Task<UsersProjectModel> relationProject(UsersProjectDTO body)
        {
            var data = this._mapper.Map<UsersProjectModel>(body);
            await this._userProjectRepository.AddChangeAsync(data);

            return data;
        }
    }
}
