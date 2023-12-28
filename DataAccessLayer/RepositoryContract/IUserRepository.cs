using DataAccessLayer.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayer.RepositoryContract
{
    /// <summary>
    /// Perpesents data access logic for managing User entity
    /// </summary>
    public interface IUserRepository
    {
        /// <summary>
        /// Adds a new user object to the data store
        /// </summary>
        /// <param name="user">User for save to BD</param>
        /// <returns>returned user with id from db</returns>
        public Task<User> AddUser(User user);

        /// <summary>
        /// Get user from db with username
        /// </summary>
        /// <param name="userName">username for search user in bd</param>
        /// <returns>returned user if find or null if not find</returns>
        public Task<User?> GetByUserName(string userName);

        /// <summary>
        /// Get user from db with id
        /// </summary>
        /// <param name="userId">guid id for search user in bd</param>
        /// <returns>returned user if find or null if not find</returns>
        public Task<User?> GetByUserId(Guid userId);
    }
}
