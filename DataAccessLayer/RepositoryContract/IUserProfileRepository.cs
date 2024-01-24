using DataAccessLayer.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayer.RepositoryContract
{
    /// <summary>
    /// Perpesents data access logic for managing UserProfile entity
    /// </summary>
    public interface IUserProfileRepository
    {
        /// <summary>
        /// Adds a new user object to the data store
        /// </summary>
        /// <param name="user">UserProfile for save to BD</param>
        /// <returns>returned user with id from db</returns>
        public Task<UserProfile?> AddUserProfileAsync(UserProfile user);

        /// <summary>
        /// Get user from db with id
        /// </summary>
        /// <param name="userId">guid id for search user in bd</param>
        /// <returns>returned user if find or null if not find</returns>
        public Task<UserProfile?> GetUserProfileByIdAsync(Guid userId);
    }
}
