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
    /// Class for implementation methods for UserProfile Service
    /// </summary>
    public class UserService : IUserService
    {
        private readonly IUserProfileRepository _userProfileRepository;
        private readonly ILogger<UserService> _logger;
        private readonly IMapper _mapper;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly SignInManager<ApplicationUser> _signInManager;
        private readonly RoleManager<ApplicationRole> _roleManager;

        public UserService(IUserProfileRepository userProfileRepository, ILogger<UserService> logger, IMapper mapper, UserManager<ApplicationUser> userManager, SignInManager<ApplicationUser> signInManager, RoleManager<ApplicationRole> roleManager)
        {
            _userProfileRepository = userProfileRepository;
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
                    _logger.LogInformation("{service}.{method} - finish, enter in system", nameof(UserService), nameof(EnterInSystemAsync));
                    return _mapper.Map<UserModel>(userEnterRequest);
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
    }
}
