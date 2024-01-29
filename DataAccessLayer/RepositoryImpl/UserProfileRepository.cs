using DataAccessLayer.DbContext;
using DataAccessLayer.Entities;
using DataAccessLayer.RepositoryContract;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayer.RepositoryImpl
{
    /// <summary>
    /// Implementation logic for user repository contract
    /// </summary>
    public class UserProfileRepository : IUserProfileRepository
    {
        private readonly ApplicationDbContext _context;

        public UserProfileRepository(ApplicationDbContext dbContext)
        {
            _context = dbContext;
        }

        public async Task<UserProfile?> AddUserProfileAsync(UserProfile user)
        {
            _context.Users.Add(user);
            await _context.SaveChangesAsync();

            return await _context.Users.FirstOrDefaultAsync(temp => temp.UserProfileId == user.UserProfileId);
        }

        public async Task<UserProfile?> GetUserProfileByIdAsync(Guid userId)
        {
            return await _context.Users.FirstOrDefaultAsync(temp => temp.UserProfileId == userId);
        }

        public async Task<UserProfile?> UpdateUserProfileAsync(UserProfile userProfile)
        {
            UserProfile? userProfileForUpdate = await _context.UserProfiles.FirstOrDefaultAsync(temp => temp.UserProfileId == userProfile.UserProfileId);

            if (userProfileForUpdate != null)
            {
                userProfileForUpdate.IsShowWeather = userProfileForUpdate.IsShowWeather;
                userProfileForUpdate.NumberPhone = userProfileForUpdate.NumberPhone;
                userProfileForUpdate.FirstName = userProfileForUpdate.FirstName;
                userProfileForUpdate.LastName = userProfileForUpdate.LastName;
                userProfileForUpdate.Age = userProfileForUpdate.Age;
                userProfileForUpdate.City = userProfileForUpdate.City;
                userProfileForUpdate.Country = userProfileForUpdate.Country;

                _context.SaveChanges();
            }

            return userProfileForUpdate;
        }
    }
}
