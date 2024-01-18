using BusinessLayer.Models.Users.Request;
using BusinessLayer.Models.Users.Response;

namespace BusinessLayer.ServiceContract
{
    /// <summary>
    /// Rerpesents business logic for manipoulating user entity
    /// </summary>
    public interface IUserService
    {
        /// <summary>
        /// Method for added logic before save in bd
        /// </summary>
        /// <param name="userRequest">data from user</param>
        /// <returns>returned user with id</returns>
        public Task<UserModel> AddUserAsync(UserAddModel userRequest);

        /// <summary>
        /// Method for check data for enter in system and returned new object with null fields if false enter in system or userresponse if true enter in system
        /// </summary>
        /// <param name="userEnterRequest">data from user for enter</param>
        /// <returns>returned new object with null fields if false enter in system or userresponse if true enter in system</returns>
        public Task<UserModel> EnterInSystemAsync(UserEnterModel userEnterRequest);

        /// <summary>
        /// Method for check exist username or not exist already in basedate
        /// </summary>
        /// <param name="userName">string field for search username</param>
        /// <returns>returned true if not exist or false or not exist</returns>
        public Task<bool> CheckUserNameAsync(string userName);
    }
}
