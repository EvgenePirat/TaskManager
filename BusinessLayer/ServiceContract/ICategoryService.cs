using BusinessLayer.Models.Categories.Request;
using BusinessLayer.Models.Categories.Response;
using BusinessLayer.Models.Tasks.Request;
using BusinessLayer.Models.Tasks.Response;

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
        public Task<CategoryModel> AddNewCategoryAsync(CategoryAddModel categoryAddRequest, string? userLogin);

        /// <summary>
        /// method for get all categories for user
        /// </summary>
        /// <param name="userId">user id for search category</param>
        /// <returns>returned all categories for user</returns>
        public Task<List<CategoryModel>> GetCategoriesForUserAsync(string? userLogin);

        /// <summary>
        /// Method for add businnes logic before delete category
        /// </summary>
        /// <param name="categoryId">guid id for check exist or not category</param>
        public Task DeleteByIdAsync(Guid categoryId);

        /// <summary>
        /// Method for add businnes logic before update category
        /// </summary>
        /// <param name="taskUpdate">category with data for update</param>
        /// <returns>returned already updated category with new data</returns>
        public Task<CategoryModel> UpdateCategoryAsync(CategoryUpdateModel categoryUpdateModel);
    }
}
