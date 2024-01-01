using BusinessLayer.DTO.Request;
using BusinessLayer.DTO.Response;
using BusinessLayer.ServiceContract;
using Microsoft.AspNetCore.Mvc;

namespace TaskManager.Controllers
{
    /// <summary>
    /// Controller with methods for working with category
    /// </summary>
    [Route("[controller]")]
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
            ViewBag.UserId = Guid.Parse(HttpContext.Session.GetString("UserId"));
            return View("AddCategory");
        }

        /// <summary>
        /// Method for get category from client and save in bd
        /// </summary>
        /// <param name="categoryAddRequest">category data from user</param>
        /// <returns>returned home page if good save or page create category with errors</returns>
        [HttpPost("[action]")]
        public async Task<IActionResult> AddNewCategoryPost(CategoryAddRequest categoryAddRequest) 
        {
            List<string> errorMessages = new List<string>();
            if (!ModelState.IsValid)
            {
                errorMessages = ModelState.Values.SelectMany(x => x.Errors).Select(x => x.ErrorMessage).ToList();

                ViewBag.Errors = errorMessages;
                return View("AddCategory");
            }

            CategoryResponse categoryResponse = await _categoryService.AddNewCategory(categoryAddRequest);

            if(categoryResponse == null)
            {
                errorMessages.Add("Error with name category!");
                ViewBag.Errors = errorMessages;
                return View("AddCategory");
            }
            
            return RedirectToAction("Home","Task");
        }
    }
}
