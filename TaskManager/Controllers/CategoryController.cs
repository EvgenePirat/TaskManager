using AutoMapper;
using BusinessLayer.Models.Categories.Request;
using BusinessLayer.ServiceContract;
using Microsoft.AspNetCore.Mvc;
using TaskManager.Dto.Categories.Request;
using TaskManager.Filteres.ActionFilter.CategoryFilters;
using TaskManager.Filteres.ErrorFilteres.CategoryErrorFilteres;

namespace TaskManager.Controllers
{
    /// <summary>
    /// Controller with methods for working with category
    /// </summary>
    [Route("[controller]")]
    public class CategoryController : Controller
    {
        private readonly ICategoryService _categoryService;
        private readonly IMapper _mapper;
        private readonly ILogger<CategoryController> _logger;

        public CategoryController(ICategoryService categoryService, IMapper mapper, ILogger<CategoryController> logger)
        {
            _categoryService = categoryService;
            _mapper = mapper;
            _logger = logger;
        }

        /// <summary>
        /// Method for show page for added new category
        /// </summary>
        /// <returns>returned page for added new category</returns>
        [HttpGet("[action]")]
        public IActionResult AddNewCategory()
        {
            _logger.LogInformation("{controller}.{method} - Get add new category page, start", nameof(CategoryController), nameof(AddNewCategory));

            string errorMessage = HttpContext.Request.Query["error"].ToString();
            if (errorMessage.Length > 0)
                ViewBag.Errors = new List<string> { errorMessage };

            _logger.LogInformation("{controller}.{method} - Get add new category page, finish", nameof(CategoryController), nameof(AddNewCategory));

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
        public async Task<IActionResult> AddNewCategoryPost(CategoryAddDto categoryAddRequest) 
        {
            _logger.LogInformation("{controller}.{method} - post category for save, start", nameof(CategoryController), nameof(AddNewCategoryPost));

            var mappedModel = _mapper.Map<CategoryAddModel>(categoryAddRequest);

            string? userLogin = User.Identity?.Name;

            await _categoryService.AddNewCategoryAsync(mappedModel, userLogin);

            _logger.LogInformation("{controller}.{method} - post category for save, finish", nameof(CategoryController), nameof(AddNewCategoryPost));

            return RedirectToAction("Home","Task");
        }
    }
}
