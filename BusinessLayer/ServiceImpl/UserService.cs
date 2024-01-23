using AutoMapper;
using BusinessLayer.Encrypted;
using BusinessLayer.Enum;
using BusinessLayer.Models.Users.Request;
using BusinessLayer.Models.Users.Response;
using BusinessLayer.ServiceContract;
using CustomExceptions.UserExceptions;
using DataAccessLayer.Entities;
using DataAccessLayer.IdentityEntities;
using DataAccessLayer.RepositoryContract;
using Microsoft.AspNet.Identity.Owin;
using Microsoft.AspNetCore.Identity;
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
        private readonly IMapper _mapper;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly SignInManager<ApplicationUser> _signInManager;
        private readonly RoleManager<ApplicationRole> _roleManager;

        public UserService(IUserRepository userRepository, ILogger<UserService> logger, IMapper mapper, UserManager<ApplicationUser> userManager, SignInManager<ApplicationUser> signInManager, RoleManager<ApplicationRole> roleManager)
        {
            _userRepository = userRepository;
            _logger = logger;
            _mapper = mapper;
            _userManager = userManager;
            _signInManager = signInManager;
            _roleManager = roleManager;
        }

        public async Task<UserModel> AddUserAsync(UserAddModel userRequest)
        {
            _logger.LogInformation("{service}.{method} - start, add new user in system", nameof(UserService), nameof(AddUserAsync));

            if(userRequest == null)
            {
                _logger.LogError("{service}.{method} - userRequest equals null", nameof(UserService), nameof(AddUserAsync));
                throw new ArgumentNullException(nameof(userRequest));
            }

            ApplicationUser user = new ApplicationUser()
            {
                UserName = userRequest.UserName,

                Email = userRequest.Email,

                DateOfBirth = userRequest.DateOfBirth
            };

            IdentityResult result = await _userManager.CreateAsync(user, userRequest.Password);

            if (result.Succeeded)
            {
                if (userRequest.UserType == UserTypes.Admin)
                {
                    await CheckExistRole(UserTypes.Admin);
                    await _userManager.AddToRoleAsync(user, UserTypes.Admin.ToString());
                }
                else
                {
                    await CheckExistRole(UserTypes.User);
                    await _userManager.AddToRoleAsync(user, UserTypes.User.ToString());
                }
                await _signInManager.SignInAsync(user, false);
            }
            else
            {
                string allError = String.Empty;
                foreach (IdentityError error in result.Errors)
                {
                    allError += error.Description + "\n";
                }
                
                throw new UserArgumentException(allError);
            }

            //User mappedUser = _mapper.Map<User>(userRequest);

            //mappedUser.Password = Md5.HashPassword(mappedUser.Password);
            //mappedUser.Age = mappedUser.CreateAccount.Year - userRequest.DateOfBirth.Year;
            
            //User? userAfterSave = await _userRepository.AddUserAsync(mappedUser);

            _logger.LogInformation("{service}.{method} - finish, add new user in system", nameof(UserService), nameof(AddUserAsync));

            return _mapper.Map<UserModel>(user);
        }

        private async Task<bool> CheckExistRole(UserTypes userTypes)
        {
            if (await _roleManager.FindByNameAsync(userTypes.ToString()) is null)
            {
                ApplicationRole applicationRole = new ApplicationRole()
                {
                    Name = userTypes.ToString()
                };
                await _roleManager.CreateAsync(applicationRole);
                return true;
            }
            else
                return false;
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
            _logger.LogInformation("{service}.{method} - start, enter in system", nameof(UserService), nameof(EnterInSystemAsync));

            if (userEnterRequest != null)
            {
                var result = await _signInManager.PasswordSignInAsync(userEnterRequest.UserName, userEnterRequest.Password, false, false);

                if(result.Succeeded)
                {
                    _logger.LogInformation("{service}.{method} - finish, enter in system", nameof(UserService), nameof(EnterInSystemAsync));
                    return _mapper.Map<UserModel>(userEnterRequest);
                }
                else
                {
                    _logger.LogError("{service}.{method} - Invalid email or password", nameof(UserService), nameof(EnterInSystemAsync));
                    throw new UserArgumentException("Invalid email or password");
                }

                //User? userAfterSearch = await _userRepository.GetByUserNameAsync(userEnterRequest.UserName);

                //if (userAfterSearch == null)
                //{
                //    _logger.LogError("{controller}.{method} - userEnterRequest not found in system!", nameof(UserService), nameof(EnterInSystemAsync));
                //    throw new UserArgumentException("UserName not found in system!");
                //}

                //var mappedUser = _mapper.Map<User>(userEnterRequest);

                //mappedUser.Password = Md5.HashPassword(mappedUser.Password);

                //if (mappedUser.Password != userAfterSearch.Password)
                //{
                //    _logger.LogError("{controller}.{method} - userEnterRequest has wrong password", nameof(UserService), nameof(EnterInSystemAsync));
                //    throw new UserArgumentException("You wrote wrong password!");
                //}

                //_logger.LogInformation("{service}.{method} - finish, enter in system", nameof(UserService), nameof(EnterInSystemAsync));

                //return _mapper.Map<UserModel>(userAfterSearch);
            }
            else
            {
                _logger.LogError("{service}.{method} - userEnterRequest equals null", nameof(UserService), nameof(EnterInSystemAsync));
                throw new ArgumentNullException(nameof(userEnterRequest));
            }
        }
    }
}
