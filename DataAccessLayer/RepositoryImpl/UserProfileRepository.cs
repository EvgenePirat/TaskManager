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
            return await _context.Users.Include("Categories").FirstOrDefaultAsync(temp => temp.UserProfileId == userId);
        }
    }
}
