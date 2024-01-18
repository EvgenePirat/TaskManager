using BusinessLayer.Mapper;
using BusinessLayer.Models.Categories.Request;
using BusinessLayer.Models.Categories.Response;
using BusinessLayer.ServiceContract;
using CustomExceptions.CategoryExceptions;
using CustomExceptions.UserExceptions;
using DataAccessLayer.Entities;
using DataAccessLayer.RepositoryContract;
using Microsoft.Extensions.Logging;

namespace BusinessLayer.ServiceImpl
{
    /// <summary>
    /// Class for implementation methods for Category Service
    /// </summary>
    public class CategoryService : ICategoryService
    {
        private readonly ICategoryRepository _categoryRepository;

        private readonly IUserRepository _userRepository;

        private readonly ILogger<CategoryService> _logger;

        public CategoryService(ICategoryRepository categoryRepository, IUserRepository userRepository, ILogger<CategoryService> logger)
        {
            _categoryRepository = categoryRepository;
            _userRepository = userRepository;
            _logger = logger;
        }

        public async Task<CategoryModel> AddNewCategoryAsync(CategoryAddModel categoryAddRequest)
        {
            if(categoryAddRequest != null)
            {
                Category? category = await _categoryRepository.GetCategoryByNameAsync(categoryAddRequest.Name);

                if (category != null)
                {
                    _logger.LogError("{service}.{method} - Category already exist", nameof(CategoryService), nameof(AddNewCategoryAsync));
                    throw new CategoryArgumentException("Category already exist!");
                }

                category = CategoryMapper.CategoryAddRequestToCategory(categoryAddRequest);
                category = await _categoryRepository.AddCategoryAsync(category);
                return CategoryMapper.CategoryToCategoryResponse(category);
            }
            else
            {
                _logger.LogError("{service}.{method} - categoryAddRequest equals null", nameof(CategoryService), nameof(AddNewCategoryAsync));
                throw new ArgumentNullException(nameof(categoryAddRequest));
            }
        }

        public async Task<List<CategoryModel>> GetCategoriesForUserAsync(Guid userId)
        {
            if((await _userRepository.GetByUserIdAsync(userId)) != null)
            {
                return (await _categoryRepository.GetAllCategoriesAsync(userId)).Select(temp => CategoryMapper.CategoryToCategoryResponse(temp)).ToList();
            }
            else
            {
                _logger.LogError("{service}.{method} - User with id not found!", nameof(CategoryService), nameof(GetCategoriesForUserAsync));
                throw new UserArgumentException("User with id not found!");
            }  
        }
    }
}
