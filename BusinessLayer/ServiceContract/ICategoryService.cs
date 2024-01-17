using BusinessLayer.DTO.CategoryDto.Request;
using BusinessLayer.DTO.CategoryDto.Response;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLayer.ServiceContract
{
    /// <summary>
    /// Rerpesents business logic for manipoulating category entity
    /// </summary>
    public interface ICategoryService
    {
        /// <summary>
        /// Method for added logic before save in bd
        /// </summary>
        /// <param name="categoryAddRequest">data abount category from client</param>
        /// <returns>returned category with id after save</returns>
        public Task<CategoryResponse> AddNewCategory(CategoryAddRequest categoryAddRequest);

        /// <summary>
        /// method for get all categories for user
        /// </summary>
        /// <param name="userId">user id for search category</param>
        /// <returns>returned all categories for user</returns>
        public Task<List<CategoryResponse>> GetCategoriesForUser(Guid userId);
    }
}
