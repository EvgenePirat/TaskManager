using AutoMapper;
using BusinessLayer.Models.Categories.Request;
using BusinessLayer.ServiceContract;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using TaskManager.Dto.Categories.Request;
using TaskManager.Dto.Categories.Response;
using TaskManager.Filteres.ErrorFilteres.CategoryErrorFilteres;

namespace TaskManager.Controllers.Mains
{
    /// <summary>
    /// Controller with methods for working with category
    /// </summary>
    [Route("[controller]")]
    [Authorize(Roles = "User, Admin")]
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
        public async Task<IActionResult> Categories()
        {
            _logger.LogInformation("{controller}.{method} - Get add new category page, start", nameof(CategoryController), nameof(Categories));

            var errorMessages = HttpContext.Request.Query["error"];
            if (errorMessages.Count > 0)
                ViewBag.Errors = new List<string> { errorMessages };

            string? userLogin = User.Identity?.Name;

            ViewBag.Categories = _mapper.Map<List<CategoryDto>>(await _categoryService.GetCategoriesForUserAsync(userLogin));

            _logger.LogInformation("{controller}.{method} - Get add new category page, finish", nameof(CategoryController), nameof(Categories));

            return View("Categories");
        }

        /// <summary>
        /// Method for get category from client and save in bd
        /// </summary>
        /// <param name="categoryAddRequest">category data from user</param>
        /// <returns>returned home page if good save or page create category with errors</returns>
        [HttpPost("[action]")]
        [TypeFilter(typeof(CategoryExceptionFilter))]
        public async Task<IActionResult> AddNewCategoryPost([FromForm] CategoryAddDto categoryAddDto)
        {
            _logger.LogInformation("{controller}.{method} - post category for save, start", nameof(CategoryController), nameof(AddNewCategoryPost));


            if (!ModelState.IsValid)
            {
                List<string> errorMessages = new List<string>();

                errorMessages = ModelState.Values.SelectMany(x => x.Errors).Select(x => x.ErrorMessage).ToList();

                ViewBag.Errors = errorMessages;
                return View("Categories");
            }

            var mappedModel = _mapper.Map<CategoryAddModel>(categoryAddDto);

            string? userLogin = User.Identity?.Name;

            await _categoryService.AddNewCategoryAsync(mappedModel, userLogin);

            ViewBag.Categories = _mapper.Map<List<CategoryDto>>(await _categoryService.GetCategoriesForUserAsync(userLogin));

            _logger.LogInformation("{controller}.{method} - post category for save, finish", nameof(CategoryController), nameof(AddNewCategoryPost));

            return View("Categories");
        }

        /// <summary>
        /// Method for update exist task
        /// </summary>
        /// <param name="categoryUpdateDto">category with new data for update</param>
        /// <returns>returned page with updates categories</returns>

        public async Task<IActionResult> UpdateCategoryPost([FromForm] CategoryUpdateDto categoryUpdateDto)
        {
            _logger.LogInformation("{controller}.{method} - post category for update, start", nameof(CategoryController), nameof(UpdateCategoryPost));

            if (!ModelState.IsValid)
            {
                List<string> errorMessages = new List<string>();

                errorMessages = ModelState.Values.SelectMany(x => x.Errors).Select(x => x.ErrorMessage).ToList();

                ViewBag.Errors = errorMessages;
                return View("Categories");
            }

            var mappedModel = _mapper.Map<CategoryUpdateModel>(categoryUpdateDto);
            await _categoryService.UpdateCategoryAsync(mappedModel);

            string? userLogin = User.Identity?.Name;

            ViewBag.Categories = _mapper.Map<List<CategoryDto>>(await _categoryService.GetCategoriesForUserAsync(userLogin));

            _logger.LogInformation("{controller}.{method} - post category for update, finish", nameof(CategoryController), nameof(UpdateCategoryPost));

            return View("Categories");
        }

        /// <summary>
        /// Method for delete category by id
        /// </summary>
        /// <param name="categoryId">guid id for delete category</param>
        /// <returns>returned categories page without category</returns>
        [HttpPost("[action]")]
        public async Task<IActionResult> DeleteCategoryPost(Guid categoryId)
        {
            _logger.LogInformation("{controller}.{method} - post delete category if exist, start", nameof(CategoryController), nameof(AddNewCategoryPost));

            await _categoryService.DeleteByIdAsync(categoryId);

            string? userLogin = User.Identity?.Name;

            ViewBag.Categories = _mapper.Map<List<CategoryDto>>(await _categoryService.GetCategoriesForUserAsync(userLogin));

            _logger.LogInformation("{controller}.{method} - post delete category if exist, finish", nameof(CategoryController), nameof(AddNewCategoryPost));

            return View("Categories");
        }
    }
}
