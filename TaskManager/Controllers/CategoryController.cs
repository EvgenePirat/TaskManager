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
        /// 
        /// </summary>
        /// <param name="categoryAddRequest"></param>
        /// <returns></returns>
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
            //i need change logic for unique for category name and i need make check for category for user with id
            return RedirectToAction("Home","Task");
        }
    }
}
