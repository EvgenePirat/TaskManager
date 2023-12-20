﻿using BusinessLayer.DTO.Request;
using BusinessLayer.DTO.Response;
using BusinessLayer.Encrypted;
using BusinessLayer.Mapper;
using BusinessLayer.ServiceContract;
using DataAccessLayer.Entities;
using DataAccessLayer.RepositoryContract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLayer.ServiceImpl
{
    public class UserService : IUserService
    {
        private readonly IUserRepository _userRepository;

        public UserService(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        public async Task<UserResponse> AddUser(UserAddRequest userRequest)
        {
            User user = UserMapper.UserRequestAddToUser(userRequest);
            user.Password = Md5.HashPassword(user.Password);
            user.CreateAccount = DateTime.Now;
            user.Age = DateTime.Now.Year - userRequest.DateOfBirth.Year;
            
            User userAfterSave = await _userRepository.AddUser(user);
            return UserMapper.UserToUserResponse(userAfterSave);
        }

        public Task<UserResponse?> EnterInSystem(UserEnterRequest userEnterRequest)
        {
            throw new NotImplementedException();
        }
    }
}