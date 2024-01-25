using DataAccessLayer.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayer.RepositoryContract
{
    /// <summary>
    /// Perpesents data access logic for managing Category entity
    /// </summary>
    public interface ICategoryRepository
    {
        /// <summary>
        /// Method for save new category in bd
        /// </summary>
        /// <param name="category">data category from user</param>
        /// <returns>returned category with id after save</returns>
        public Task<Category> AddCategoryAsync(Category category);

        /// <summary>
        /// Method for get all categories for user
        /// </summary>
        /// <param name="UserId">user id for filtered categories</param>
        /// <returns>returned all categories for user</returns>
        public Task<List<Category>> GetAllCategoriesAsync(Guid UserId);

        /// <summary>
        /// Method for find and get category by category name
        /// </summary>
        /// <param name="categoryName">string name for search category</param>
        /// <returns>returned category if find or null if not find</returns>
        public Task<Category?> GetCategoryByNameAsync(string categoryName);

        /// <summary>
        /// Method for find and get category with by id
        /// </summary>
        /// <param name="Id">guid id for search category</param>
        /// <returns>returned category if find or null if not find</returns>
        public Task<Category?> GetCategoryByIdAsync(Guid Id);

        /// <summary>
        /// Method for delete category by id
        /// </summary>
        /// <param name="categoryId">guid id for delete category</param>
        public System.Threading.Tasks.Task DeleteCategoryByIdAsync(Guid id);
    }
}
