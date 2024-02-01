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
                if (userRequest.UserType == RoleTypes.Admin)
                {
                    await CheckExistRole(RoleTypes.Admin);
                    await _signInManager.UserManager.AddToRoleAsync(user, RoleTypes.Admin.ToString());
                }
                else
                {
                    await CheckExistRole(RoleTypes.User);
                    await _signInManager.UserManager.AddToRoleAsync(user, RoleTypes.User.ToString());
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

        private async Task<bool> CheckExistRole(RoleTypes userTypes)
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
            {
                _logger.LogError("{service}.{method} - You need authorization in application", nameof(UserService), nameof(UpdateUserProfileAsync));
                throw new AuthorizationArgumentException("You need authorization in application");
            }

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

            int age = DateTime.Today.Year - userProfileModel.DateOfBirth.Year;

            mapperUserProfile.UserProfileId = userProfileModel.Id;
            mapperUserProfile.Age = age;

            var updatedUserProfile = await _userProfileRepository.UpdateUserProfileAsync(mapperUserProfile);

            userProfileModel.Age = age;

            var applicationUserForUpdate = await _signInManager.UserManager.FindByIdAsync(userProfileModel.Id.ToString()) ?? throw new AuthorizationArgumentException("You need authorization in application");

            string currentUserName = applicationUserForUpdate.UserName;

            applicationUserForUpdate.UserName = userProfileModel.UserName;
            applicationUserForUpdate.Email = userProfileModel.Email;
            applicationUserForUpdate.DateOfBirth = userProfileModel.DateOfBirth;

            var result = await _signInManager.UserManager.UpdateAsync(applicationUserForUpdate);

            if(currentUserName != applicationUserForUpdate.UserName)
                await _signInManager.SignInAsync(applicationUserForUpdate, false);

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
            {
                _logger.LogError("{service}.{method} - You need authorization in application", nameof(UserService), nameof(UpdateUserProfileAsync));
                throw new AuthorizationArgumentException("You need authorization in application");
            }
                

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

        public async System.Threading.Tasks.Task LogoutFromSystemAsync()
        {
            _logger.LogInformation("{service}.{method} - start, logout from system in service layer", nameof(UserService), nameof(LogoutFromSystemAsync));

            await _signInManager.SignOutAsync();

            _logger.LogInformation("{service}.{method} - finish, logout from system in service layer", nameof(UserService), nameof(LogoutFromSystemAsync));
        }

        public async Task<bool> DeleteUserById(Guid id)
        {
            _logger.LogInformation("{service}.{method} - start, delete application user in service layer", nameof(UserService), nameof(DeleteUserById));

            var userToDelete = await _signInManager.UserManager.FindByIdAsync(id.ToString());

            if (userToDelete != null)
            {
                var result = await _signInManager.UserManager.DeleteAsync(userToDelete);

                if (result.Succeeded)
                {
                    _logger.LogInformation("{service}.{method} - finish, delete application user in service layer", nameof(UserService), nameof(DeleteUserById));

                    return true;
                }
                else
                {
                    var errors = string.Join(", ", result.Errors.Select(error => error.Description));

                    _logger.LogError($"{nameof(UserService)}.{nameof(UpdateUserProfileAsync)} - {errors}");

                    throw new UserArgumentException(errors);
                }
            }
            else
            {
                _logger.LogError("{service}.{method} - User for delete by id not found!", nameof(UserService), nameof(UpdateUserProfileAsync));
                throw new UserArgumentException("User for delete by id not found!");
            }   
        }

        public List<ApplicationUserModel> GetAllApplicationUsers()
        {
            _logger.LogInformation("{service}.{method} - start, get all application user in service layer", nameof(UserService), nameof(DeleteUserById));

            var allUsers = _signInManager.UserManager.Users.ToList();

            _logger.LogInformation("{service}.{method} - finish, get all application user in service layer", nameof(UserService), nameof(DeleteUserById));

            return _mapper.Map<List<ApplicationUserModel>>(allUsers);
        }
    }
}
