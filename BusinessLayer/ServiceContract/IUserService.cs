﻿using BusinessLayer.Models.Users.Request;
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
        /// Method for get full profile for user by login
        /// </summary>
        /// <param name="userLogin">string login for search user</param>
        /// <returns>returned user profile model with all information about user</returns>
        public Task<UserProfileModel> GetUserProfileByLoginAsync(string? userLogin);

        /// <summary>
        /// Method for get full profile for user by id
        /// </summary>
        /// <param name="userLogin">guid id for search user</param>
        /// <returns>returned user profile model with all information about user</returns>
        public Task<UserProfileModel> GetUserProfileByIdAsync(Guid userId);

        /// <summary>
        /// Method for update user profile and user application
        /// </summary>
        /// <param name="userProfileModel">User profile model with new data for update</param>
        /// <returns>returned model with updated data</returns>
        public Task<UserProfileModel> UpdateUserProfileAsync(UserProfileModel userProfileModel);

        /// <summary>
        /// Method for chec field isShowWeather for user
        /// </summary>
        /// <param name="userLogin">login user for search</param>
        /// <returns>returned true or false</returns>
        public Task<UserWeatherProfileModel> GetUserWeatherProfileAsync(string? userLogin);

        /// <summary>
        /// Method for logout from system for user
        /// </summary>
        public Task LogoutFromSystemAsync();

        /// <summary>
        /// Method for delete application user
        /// </summary>
        /// <param name="id">guid id for search and delete user</param>
        public Task<bool> DeleteUserById(Guid id);


        /// <summary>
        /// Method for get all users in application
        /// </summary>
        /// <returns>returned list application users</returns>
        public List<ApplicationUserModel> GetAllApplicationUsers();

        /// <summary>
        /// Method for post new password on email
        /// </summary>
        /// <param name="userRestore">object for search user and post new password on email</param>
        /// <returns>returned true if all updated or false if not post</returns>
        public Task<bool> RestorePasswordForUser(UserRestoreModel userRestore);

    }
}
