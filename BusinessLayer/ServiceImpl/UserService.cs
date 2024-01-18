using BusinessLayer.Encrypted;
using BusinessLayer.Mapper;
using BusinessLayer.Models.Users.Request;
using BusinessLayer.Models.Users.Response;
using BusinessLayer.ServiceContract;
using CustomExceptions.UserExceptions;
using DataAccessLayer.Entities;
using DataAccessLayer.RepositoryContract;
using Microsoft.Extensions.Logging;

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

        public async Task<UserModel> AddUserAsync(UserAddModel userRequest)
        {
            _logger.LogInformation("{service}.{method} - add new user in system", nameof(UserService), nameof(AddUserAsync));

            if(userRequest == null)
            {
                _logger.LogError("{service}.{method} - userRequest equals null", nameof(UserService), nameof(AddUserAsync));
                throw new ArgumentNullException(nameof(userRequest));
            }

            User user = UserMapper.UserRequestAddToUser(userRequest);
            user.Password = Md5.HashPassword(user.Password);
            user.Age = user.CreateAccount.Year - userRequest.DateOfBirth.Year;
            
            User userAfterSave = await _userRepository.AddUserAsync(user);
            return UserMapper.UserToUserResponse(userAfterSave);
        }

        public async Task<bool> CheckUserNameAsync(string userName)
        {
            _logger.LogInformation("{service}.{method} - start, check user name in system", nameof(UserService), nameof(CheckUserNameAsync));

            User? user = await _userRepository.GetByUserNameAsync(userName);

            _logger.LogInformation("{service}.{method} - finish, check user name in system", nameof(UserService), nameof(CheckUserNameAsync));
            return user == null;
        }

        public async Task<UserModel> EnterInSystemAsync(UserEnterModel userEnterRequest)
        {
            _logger.LogInformation("{service}.{method} - enter in system", nameof(UserService), nameof(EnterInSystemAsync));

            if (userEnterRequest != null)
            {
                if (await CheckUserNameAsync(userEnterRequest.UserName))
                {
                    _logger.LogError("{controller}.{method} - userEnterRequest not found in system!", nameof(UserService), nameof(EnterInSystemAsync));
                    throw new UserArgumentException("UserName not found in system!");
                }

                User? userAfterSearch = await _userRepository.GetByUserNameAsync(userEnterRequest.UserName);

                userEnterRequest.Password = Md5.HashPassword(userEnterRequest.Password);

                if (userEnterRequest.Password != userAfterSearch.Password)
                {
                    _logger.LogError("{controller}.{method} - userEnterRequest has wrong password", nameof(UserService), nameof(EnterInSystemAsync));
                    throw new UserArgumentException("You wrote wrong password!");
                }

                return UserMapper.UserToUserResponse(userAfterSearch);
            }
            else
            {
                _logger.LogError("{service}.{method} - userEnterRequest equals null", nameof(UserService), nameof(EnterInSystemAsync));
                throw new ArgumentNullException(nameof(userEnterRequest));
            }
        }
    }
}
