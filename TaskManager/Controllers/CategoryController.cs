using BusinessLayer.DTO.CategoryDto.Request;
using BusinessLayer.ServiceContract;
using Microsoft.AspNetCore.Mvc;
using TaskManager.Filteres.ActionFilter.CategoryFilters;
using TaskManager.Filteres.AuthorizationFilter;
using TaskManager.Filteres.ErrorFilteres.CategoryErrorFilteres;

namespace TaskManager.Controllers
{
    /// <summary>
    /// Controller with methods for working with category
    /// </summary>
    [Route("[controller]")]
    [TypeFilter(typeof(AuthorizationFilter))]
    public class CategoryController : Controller
    {
        private readonly ICategoryService _categoryService;

        public CategoryController(ICategoryService categoryService)
        {
            _categoryService = categoryService;
        }

        /// <summary>
        /// Method for show page for added new category
        /// </summary>
        /// <returns>returned page for added new category</returns>
        [HttpGet("[action]")]
        public IActionResult AddNewCategory()
        {
            string errorMessage = HttpContext.Request.Query["error"].ToString();
            if (errorMessage.Length > 0)
                ViewBag.Errors = new List<string> { errorMessage };

            ViewBag.UserId = Guid.Parse(HttpContext.Session.GetString("UserId"));
            return View("AddCategory");
        }

        /// <summary>
        /// Method for get category from client and save in bd
        /// </summary>
        /// <param name="categoryAddRequest">category data from user</param>
        /// <returns>returned home page if good save or page create category with errors</returns>
        [HttpPost("[action]")]
        [TypeFilter(typeof(CategoryValidationActionFilter))]
        [TypeFilter(typeof(CategoryExceptionFilter))]
        public async Task<IActionResult> AddNewCategoryPost(CategoryAddRequest categoryAddRequest) 
        {
            await _categoryService.AddNewCategory(categoryAddRequest);
            
            return RedirectToAction("Home","Task");
        }
    }
}
