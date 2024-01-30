using AutoMapper;
using BusinessLayer.Models.Enum;
using BusinessLayer.Models.Users.Request;
using BusinessLayer.Models.Users.Response;
using BusinessLayer.ServiceContract;
using CustomExceptions.AuthorizationExceptions;
using CustomExceptions.UserExceptions;
using DataAccessLayer.Entities;
using DataAccessLayer.IdentityEntities;
using DataAccessLayer.RepositoryContract;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Logging;

namespace BusinessLayer.ServiceImpl
{
    /// <summary>
    /// Class for implementation methods for UserProfile Service
    /// </summary>
    public class UserService : IUserService
    {
        private readonly IUserProfileRepository _userProfileRepository;
        private readonly ILogger<UserService> _logger;
        private readonly IMapper _mapper;
        private readonly SignInManager<ApplicationUser> _signInManager;
        private readonly RoleManager<ApplicationRole> _roleManager;

        public UserService(IUserProfileRepository userProfileRepository, ILogger<UserService> logger, IMapper mapper, SignInManager<ApplicationUser> signInManager, RoleManager<ApplicationRole> roleManager)
        {
            _userProfileRepository = userProfileRepository;
            _logger = logger;
            _mapper = mapper;
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

            int age = DateTime.Today.Year - userRequest.DateOfBirth.Year;

            ApplicationUser user = new ApplicationUser()
            {
                UserName = userRequest.UserName,

                Email = userRequest.Email,

                DateOfBirth = userRequest.DateOfBirth,

                UserProfile = new UserProfile()
                {
                    Age = age
                }
            };

            IdentityResult result = await _signInManager.UserManager.CreateAsync(user, userRequest.Password);

            if (result.Succeeded)
            {
                if (userRequest.UserType == UserTypes.Admin)
                {
                    await CheckExistRole(UserTypes.Admin);
                    await _signInManager.UserManager.AddToRoleAsync(user, UserTypes.Admin.ToString());
                }
                else
                {
                    await CheckExistRole(UserTypes.User);
                    await _signInManager.UserManager.AddToRoleAsync(user, UserTypes.User.ToString());
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

        public async Task<UserModel> EnterInSystemAsync(UserEnterModel userEnterRequest)
        {
            _logger.LogInformation("{service}.{method} - start, enter in system", nameof(UserService), nameof(EnterInSystemAsync));

            if (userEnterRequest != null)
            {
                var result = await _signInManager.PasswordSignInAsync(userEnterRequest.UserName, userEnterRequest.Password, false, false);

                if(result.Succeeded)
                {
                    var user = _signInManager.UserManager.Users.FirstOrDefault(temp => temp.UserName == userEnterRequest.UserName);
                    _logger.LogInformation("{service}.{method} - finish, enter in system", nameof(UserService), nameof(EnterInSystemAsync));
                    return _mapper.Map<UserModel>(user);
                }
                else
                {
                    _logger.LogError("{service}.{method} - Invalid email or password", nameof(UserService), nameof(EnterInSystemAsync));
                    throw new UserArgumentException("Invalid email or password");
                }
            }
            else
            {
                _logger.LogError("{service}.{method} - userEnterRequest equals null", nameof(UserService), nameof(EnterInSystemAsync));
                throw new ArgumentNullException(nameof(userEnterRequest));
            }
        }

        public async Task<UserProfileModel> GetUserProfileAsync(string? userLogin)
        {
            _logger.LogInformation("{service}.{method} - start, get user profile by user login", nameof(UserService), nameof(GetUserProfileAsync));

            if (userLogin == null)
                throw new AuthorizationArgumentException("You need authorization in application");

            var userApplication = await _signInManager.UserManager.FindByNameAsync(userLogin) ?? throw new AuthorizationArgumentException("You need authorization in application"); ;
            var userProfile = await _userProfileRepository.GetUserProfileByIdAsync(userApplication.Id) ?? throw new AuthorizationArgumentException("You need authorization in application"); ;  

            var userProfileModel = new UserProfileModel()
            {
                Age = userProfile.Age,
                City = userProfile.City,
                Country = userProfile.Country,
                DateOfBirth = userApplication.DateOfBirth,
                Email = userApplication.Email,
                Id = userApplication.Id,
                UserName = userApplication.UserName,
                FirstName = userProfile.FirstName,
                LastName = userProfile.LastName,
                NumberPhone = userProfile.NumberPhone,
                IsShowWeather = userProfile.IsShowWeather ?? false,  
            };

            _logger.LogInformation("{service}.{method} - finish, get user profile by user login", nameof(UserService), nameof(GetUserProfileAsync));

            return userProfileModel;
        }

        public async Task<UserProfileModel> UpdateUserProfileAsync(UserProfileModel userProfileModel)
        {
            _logger.LogInformation("{service}.{method} - start, update user profile and user application", nameof(UserService), nameof(UpdateUserProfileAsync));

            var mapperUserProfile = _mapper.Map<UserProfile>(userProfileModel);

            mapperUserProfile.UserProfileId = userProfileModel.Id;

            var updatedUserProfile = await _userProfileRepository.UpdateUserProfileAsync(mapperUserProfile);

            var applicationUserForUpdate = await _signInManager.UserManager.FindByIdAsync(userProfileModel.Id.ToString()) ?? throw new AuthorizationArgumentException("You need authorization in application");

            applicationUserForUpdate.UserName = userProfileModel.UserName;
            applicationUserForUpdate.Email = userProfileModel.Email;
            applicationUserForUpdate.DateOfBirth = userProfileModel.DateOfBirth;

            var result = await _signInManager.UserManager.UpdateAsync(applicationUserForUpdate);

            if (result.Succeeded)
            {
                _logger.LogInformation("{service}.{method} - finish, update user profile and user application", nameof(UserService), nameof(UpdateUserProfileAsync));
                return userProfileModel;
            }
            else
            {
                _logger.LogError("{service}.{method} - We can not update application user", nameof(UserService), nameof(UpdateUserProfileAsync));
                throw new UserArgumentException("We can not update application user");
            }
               
        }

        public async Task<UserWeatherProfileModel> GetUserWeatherProfileAsync(string? userLogin)
        {
            _logger.LogInformation("{service}.{method} - start, check field isShowWeather for user", nameof(UserService), nameof(GetUserWeatherProfileAsync));

            if (userLogin == null)
                throw new AuthorizationArgumentException("You need authorization in application");

            var applicationUser = await _signInManager.UserManager.FindByNameAsync(userLogin);

            var userProfile = await _userProfileRepository.GetUserProfileByIdAsync(applicationUser.Id);

            var result = new UserWeatherProfileModel()
            {
                Id = applicationUser.Id,
                City = userProfile?.City,
                Country = userProfile?.Country,
                IsShowWeather = userProfile?.IsShowWeather ?? false
            };

            _logger.LogInformation("{service}.{method} - finish, check field isShowWeather for user", nameof(UserService), nameof(GetUserWeatherProfileAsync));

            return result;
        }
    }
}
