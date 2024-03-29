﻿using DataAccessLayer.DbContext;
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
    /// Implementation logic for category repository contract
    /// </summary>
    public class CategoryRepository : ICategoryRepository
    {
        private readonly ApplicationDbContext _context;

        public CategoryRepository(ApplicationDbContext dbContext)
        {
            _context = dbContext;
        }

        public async Task<Category?> AddCategoryAsync(Category category)
        {
            _context.Categories.Add(category);
            await _context.SaveChangesAsync();

            return await _context.Categories.Include("Tasks").FirstOrDefaultAsync(temp => temp.Id == category.Id);
        }

        public async System.Threading.Tasks.Task DeleteCategoryByIdAsync(Guid categoryId)
        {
            var categoryDelete = await _context.Categories.FindAsync(categoryId);
            _context.Categories.Remove(categoryDelete);
            await _context.SaveChangesAsync();
        }

        public async Task<List<Category>> GetAllCategoriesAsync(Guid UserId)
        {
            return await _context.Categories.Include("Tasks").Where(temp => temp.UserId == UserId).ToListAsync();
        }

        public async Task<Category?> GetCategoryByIdAsync(Guid Id)
        {
            return await _context.Categories.FirstOrDefaultAsync(temp => temp.Id == Id);
        }

        public async Task<Category?> GetCategoryByNameAsync(string categoryName)
        {
            return await _context.Categories.FirstOrDefaultAsync(temp => temp.Name == categoryName);
        }

        public async Task<Category?> UpdateCategoryAsync(Category category)
        {
            Category? categoryFromDB = await _context.Categories.FirstOrDefaultAsync(temp => temp.Id == category.Id);

            if(categoryFromDB != null)
            {
                categoryFromDB.Name = category.Name;
                _context.Categories.Update(categoryFromDB);
                await _context.SaveChangesAsync();
            }

            return categoryFromDB;
        }
    }
}
