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
    public class UserRepository : IUserRepository
    {
        private readonly ApplicationDbContext _context;

        public UserRepository(ApplicationDbContext dbContext)
        {
            _context = dbContext;
        }

        public async Task<User?> AddUserAsync(User user)
        {
            _context.Users.Add(user);
            await _context.SaveChangesAsync();

            return await _context.Users.Include("Role").FirstOrDefaultAsync(temp => temp.Id == user.Id);
        }

        public async Task<User?> GetByUserNameAsync(string userName)
        {
            return await _context.Users.Include("Role").FirstOrDefaultAsync(temp => temp.UserName == userName);
        }
    }
}
