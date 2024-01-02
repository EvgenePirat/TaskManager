using BusinessLayer.DTO.Request;
using BusinessLayer.DTO.Response;
using BusinessLayer.Encrypted;
using BusinessLayer.Mapper;
using BusinessLayer.ServiceContract;
using DataAccessLayer.Entities;
using DataAccessLayer.RepositoryContract;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLayer.ServiceImpl
{
    /// <summary>
    /// Class for implementation methods for User Service
    /// </summary>
    public class UserService : IUserService
    {
        private readonly IUserRepository _userRepository;
        private readonly ILogger<UserService> _logger;

        public UserService(IUserRepository userRepository, ILogger<UserService> logger)
        {
            _userRepository = userRepository;
            _logger = logger;
        }

        public async Task<UserResponse> AddUser(UserAddRequest userRequest)
        {
            _logger.LogInformation("{service}.{method} - add new user in system", nameof(UserService), nameof(AddUser));

            if(userRequest == null)
            {
                _logger.LogError("{service}.{method} - userRequest equals null", nameof(UserService), nameof(AddUser));
                throw new ArgumentNullException(nameof(userRequest));
            }

            User user = UserMapper.UserRequestAddToUser(userRequest);
            user.Password = Md5.HashPassword(user.Password);
            user.Age = user.CreateAccount.Year - userRequest.DateOfBirth.Year;
            
            User userAfterSave = await _userRepository.AddUser(user);
            return UserMapper.UserToUserResponse(userAfterSave);
        }

        public async Task<bool> CheckUserName(string userName)
        {
            User? user = await _userRepository.GetByUserName(userName);
            return user == null;
        }

        public async Task<UserResponse?> EnterInSystem(UserEnterRequest userEnterRequest)
        {
            if(userEnterRequest != null)
            {
                User? userAfterSearch = await _userRepository.GetByUserName(userEnterRequest.UserName);

                userEnterRequest.Password = Md5.HashPassword(userEnterRequest.Password);

                if (userEnterRequest.Password == userAfterSearch.Password)
                    return UserMapper.UserToUserResponse(userAfterSearch);
            }
            return null;
        }
    }
}
