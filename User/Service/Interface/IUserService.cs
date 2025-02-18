﻿using User.DTOs;
using User.Model;

namespace User.Service.Interface
{
    public interface IUserService
    {
        Task<UserModel> FindByValue(string key, object value);
        Task<IEnumerable<UserModel>> GetAll();
        Task<UserModel> GetById(int id);
        Task<UserModel> RegisterUser(CreateUserDTO body);
        Task<UserModel> UpdateToken(int id, string RefreshToken);
        Task<UserModel> UpdateUser(int id, UpdateUserDTO body);
        Task DelteUser(int id);
    }
}
