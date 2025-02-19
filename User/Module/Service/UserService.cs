using AutoMapper;
using Microsoft.AspNetCore.Identity;
using User.Module.DTOs;
using User.Module.Model;
using User.Module.Repository.Interface;
using User.Module.Service.Interface;
using User.Utils.Exceptions;

namespace User.Module.Service
{
    public class UserService : IUserService
    {
        private readonly IUserRepository _repository;
        private readonly IMapper _mapper;


        public UserService(IUserRepository repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        /// <summary>
        /// Delete a User by ID
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public async Task DelteUser(int id)
        {
            var user = await GetById(id);
            await _repository.RemoveAsync(user);
        }

        /// <summary>
        /// Fin by value key
        /// </summary>
        /// <param name="key"></param>
        /// <param name="value"></param>
        /// <returns></returns>
        /// <exception cref="NotFoundException"></exception>
        public async Task<UserModel> FindByValue(string key, object value) {
            var user = await _repository.FindByKey(key, value);
            if (user == null) throw new NotFoundException("Value not found");
            return user;
        }

        /// <summary>
        /// Get all User register
        /// </summary>
        /// <returns></returns>
        public async Task<IEnumerable<UserModel>> GetAll()
        {
            return await _repository.ToListAsync();
        }

        /// <summary>
        /// Get User By ID
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        /// <exception cref="NotFoundException"></exception>
        public async Task<UserModel> GetById(int id)
        {
            var user = await _repository.FindByIdAsync(id);
            if (user == null)
                throw new NotFoundException("User not found");

            return user;
        }
        /// <summary>
        /// Register a new User
        /// </summary>
        /// <param name="body"></param>
        /// <returns></returns>
        /// <exception cref="ConflictExceptions"></exception>
        public async Task<UserModel> RegisterUser(CreateUserDTO body)
        {
            if (_repository.ExistsByUsername(body.Username)) throw new ConflictExceptions ("The username already exists");

            if (_repository.ExistsByEmail(body.Email)) throw new ConflictExceptions ("The Email already exists");

            var date = _mapper.Map<UserModel>(body);

            var passwordHaser = new PasswordHasher<UserModel>();
            date.Password = passwordHaser.HashPassword(date, body.Password);

            await _repository.AddChangeAsync(date);
            return date;
        }

        /// <summary>
        /// Update RefreshToken
        /// </summary>
        /// <param name="id"></param>
        /// <param name="RefreshToken"></param>
        /// <returns></returns>
        public async Task<UserModel> UpdateToken(int id, string RefreshToken) {
            var user = await GetById(id);
            user.RefreshToken = RefreshToken;

            await _repository.UpdateAsync(user);
            return user;
        }

        /// <summary>
        /// Update User Register
        /// </summary>
        /// <param name="id"></param>
        /// <param name="body"></param>
        /// <returns></returns>
        public async Task<UserModel> UpdateUser(int id, UpdateUserDTO body)
        {
            var user = await GetById(id);
            _mapper.Map(body, user);

            await _repository.UpdateAsync(user);
            return user;
        }
    }
}
