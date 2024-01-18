using BusinessLayer.Models.Categories.Request;
using BusinessLayer.Models.Categories.Response;

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
        public Task<CategoryModel> AddNewCategoryAsync(CategoryAddModel categoryAddRequest);

        /// <summary>
        /// method for get all categories for user
        /// </summary>
        /// <param name="userId">user id for search category</param>
        /// <returns>returned all categories for user</returns>
        public Task<List<CategoryModel>> GetCategoriesForUserAsync(Guid userId);
    }
}
