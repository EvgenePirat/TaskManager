﻿using DataAccessLayer.Entities;
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
    }
}