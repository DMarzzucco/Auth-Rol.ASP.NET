using AutoMapper;
using Microsoft.AspNetCore.Identity;
using User.DTOs;
using User.Model;
using User.Repository.Interface;
using User.Service.Interface;
using User.Utils.Exceptions;

namespace User.Service
{
    public class UserService : IUserService
    {
        private readonly IUserRepository _repository;
        private readonly IMapper _mapper;


        public UserService(IUserRepository repository, IMapper mapper)
        {
            this._repository = repository;
            this._mapper = mapper;
        }


        public async Task DelteUser(int id)
        {
            var user = await GetById(id);
            await this._repository.RemoveAsync(user);
        }

        public async Task<IEnumerable<UserModel>> GetAll()
        {
            return await this._repository.ToListAsync();
        }

        public async Task<UserModel> GetById(int id)
        {
            var user = await this._repository.FindByIdAsync(id);
            if (user == null)
                throw new NotFoundException("User not found");

            return user;
        }

        public async Task<UserModel> RegisterUser(CreateUserDTO body)
        {
            if (this._repository.ExistsByUsername(body.Username)) throw new ConflictExceptions ("The username already exists");

            if (this._repository.ExistsByEmail(body.Email)) throw new ConflictExceptions ("The Email already exists");

            var date = this._mapper.Map<UserModel>(body);

            var passwordHaser = new PasswordHasher<UserModel>();
            date.Password = passwordHaser.HashPassword(date, body.Password);

            await this._repository.AddChangeAsync(date);
            return date;
        }

        public async Task<UserModel> UpdateUser(int id, UpdateUserDTO body)
        {
            var user = await GetById(id);
            this._mapper.Map(body, user);

            await this._repository.UpdateAsync(user);
            return user;
        }
    }
}
