using DataAccessLayer.Entities;
using DataAccessLayer.RepositoryContract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayer.RepositoryImpl
{
    public class RoleRepository : IRoleRepository
    {
        private readonly ApplicationDbContext _context;

        public RoleRepository(ApplicationDbContext dbContext)
        {
            _context = dbContext;
        }

        public List<Role> GetAllRoles()
        {
            return _context.Roles.ToList();
        }
    }
}
