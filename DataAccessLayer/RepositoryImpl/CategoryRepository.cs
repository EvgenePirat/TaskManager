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
    public class CategoryRepository : ICategoryRepository
    {
        private readonly ApplicationDbContext _context;

        public CategoryRepository(ApplicationDbContext dbContext)
        {
            _context = dbContext;
        }

        public async Task<Category?> AddCategory(Category category)
        {
            _context.Categories.Add(category);
            await _context.SaveChangesAsync();

            return await _context.Categories.Include("Tasks").FirstOrDefaultAsync(temp => temp.Id == category.Id);
        }

        public async Task<List<Category>> GetAllCategories(Guid UserId)
        {
            return await _context.Categories.Include("Tasks").Where(temp => temp.UserId == UserId).ToListAsync();
        }
    }
}
