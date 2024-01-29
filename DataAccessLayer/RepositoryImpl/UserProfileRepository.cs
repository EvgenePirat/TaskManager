using DataAccessLayer.DbContext;
using DataAccessLayer.Entities;
using DataAccessLayer.RepositoryContract;
using Microsoft.EntityFrameworkCore;

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
            _context.UserProfiles.Add(user);
            await _context.SaveChangesAsync();

            return await _context.UserProfiles.FirstOrDefaultAsync(temp => temp.UserProfileId == user.UserProfileId);
        }

        public async Task<UserProfile?> GetUserProfileByIdAsync(Guid userId)
        {
            return await _context.UserProfiles.FirstOrDefaultAsync(temp => temp.UserProfileId == userId);
        }

        public async Task<UserProfile?> UpdateUserProfileAsync(UserProfile userProfile)
        {
            UserProfile? userProfileForUpdate = await _context.UserProfiles.FirstOrDefaultAsync(temp => temp.UserProfileId == userProfile.UserProfileId);

            if (userProfileForUpdate != null)
            {
                userProfileForUpdate.IsShowWeather = userProfile.IsShowWeather;
                userProfileForUpdate.NumberPhone = userProfile.NumberPhone;
                userProfileForUpdate.FirstName = userProfile.FirstName;
                userProfileForUpdate.LastName = userProfile.LastName;
                userProfileForUpdate.Age = userProfile.Age;
                userProfileForUpdate.City = userProfile.City;
                userProfileForUpdate.Country = userProfile.Country;

                _context.SaveChanges();
            }

            return userProfileForUpdate;
        }
    }
}
