using BusinessLayer.DTO.Request;
using BusinessLayer.DTO.Response;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
        public Task<UserResponse> AddUser(UserAddRequest userRequest);

        /// <summary>
        /// Method for check data for enter in system and returned null if false enter in system or userresponse if true enter in system
        /// </summary>
        /// <param name="userEnterRequest">data from user for enter</param>
        /// <returns>returned null if false enter in system or userresponse if true enter in system</returns>
        public Task<UserResponse?> EnterInSystem(UserEnterRequest userEnterRequest);
    }
}
