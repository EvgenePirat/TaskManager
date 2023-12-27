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
        /// <param name="category">fata category from user</param>
        /// <returns>returned category with id after save</returns>
        public Task<Category> AddCategory(Category category);

        /// <summary>
        /// Method for get all categories for user
        /// </summary>
        /// <param name="UserId">user id for filtered categories</param>
        /// <returns>returned all categories for user</returns>
        public Task<List<Category>> GetAllCategories(Guid UserId);
    }
}
