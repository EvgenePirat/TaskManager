using BusinessLayer.DTO.UserDto.Request;
using BusinessLayer.DTO.UserDto.Response;
using DataAccessLayer.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLayer.Mapper
{
    /// <summary>
    /// Class mapper allows change class on other class with save data for user entity
    /// </summary>
    public static class UserMapper
    {
        /// <summary>
        /// Method with logic for change from user class to userResponse class
        /// </summary>
        /// <param name="user">user has data for userresponse</param>
        /// <returns>returned userresponse with data from user</returns>
        public static UserResponse UserToUserResponse(User user)
        {
            return new UserResponse() { Id = user.Id, Name=user.Name, Email=user.Email,Age=user.Age, Password=user.Password, UserName=user.UserName, Role=RoleMapper.RoleToRoleResponse(user.Role) };
        }

        /// <summary>
        /// Method with logic for change from useraddrequest class to user class
        /// </summary>
        /// <param name="addRequest">useraddrequest has data for user</param>
        /// <returns>returned user with data from useraddrequest</returns>
        public static User UserRequestAddToUser(UserAddRequest addRequest)
        {
            return new User() { Email = addRequest.Email, Name = addRequest.Name, UserName = addRequest.UserName, RoleId = addRequest.RoleId, Password = addRequest.Password, DateOfBirth = addRequest.DateOfBirth };
        }
    }
}
