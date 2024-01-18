using AutoMapper;
using BusinessLayer.Models.Categories.Request;
using BusinessLayer.Models.Categories.Response;
using BusinessLayer.ServiceContract;
using CustomExceptions.CategoryExceptions;
using CustomExceptions.UserExceptions;
using DataAccessLayer.Entities;
using DataAccessLayer.RepositoryContract;
using Microsoft.Extensions.Logging;
using System.ComponentModel;

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
        private readonly IMapper _mapper;

        public CategoryService(ICategoryRepository categoryRepository, IUserRepository userRepository, ILogger<CategoryService> logger, IMapper mapper)
        {
            _categoryRepository = categoryRepository;
            _userRepository = userRepository;
            _logger = logger;
            _mapper = mapper;
        }

        public async Task<CategoryModel> AddNewCategoryAsync(CategoryAddModel categoryAddRequest)
        {
            _logger.LogInformation("{service}.{method} - start, add new category in service layer", nameof(CategoryService), nameof(AddNewCategoryAsync));

            if (categoryAddRequest != null)
            {
                if (await CheckNameCategoryForUserAsync(categoryAddRequest.Name, categoryAddRequest.UserId))
                {
                    _logger.LogError("{service}.{method} - Category already exist", nameof(CategoryService), nameof(AddNewCategoryAsync));
                    throw new CategoryArgumentException("Category already exist!");
                }

                var mappedCategory = _mapper.Map<Category>(categoryAddRequest);
                Category category = await _categoryRepository.AddCategoryAsync(mappedCategory);

                _logger.LogInformation("{service}.{method} - finish, add new category in service layer", nameof(CategoryService), nameof(AddNewCategoryAsync));

                return _mapper.Map<CategoryModel>(category);
            }
            else
            {
                _logger.LogError("{service}.{method} - categoryAddRequest equals null", nameof(CategoryService), nameof(AddNewCategoryAsync));
                throw new ArgumentNullException(nameof(categoryAddRequest));
            }
        }

        private async Task<bool> CheckNameCategoryForUserAsync(string categoryName, Guid userId)
        {
            var categories = await GetCategoriesForUserAsync(userId);
            CategoryModel? categoryFind = categories.FirstOrDefault(temp =>  temp.Name == categoryName);
            return categoryFind != null;
        }

        public async Task<List<CategoryModel>> GetCategoriesForUserAsync(Guid userId)
        {
            _logger.LogInformation("{service}.{method} - start, get category for user in service layer", nameof(CategoryService), nameof(GetCategoriesForUserAsync));

            if ((await _userRepository.GetByUserIdAsync(userId)) != null)
            {
                var categories = _mapper.Map<List<CategoryModel>>(await _categoryRepository.GetAllCategoriesAsync(userId));

                _logger.LogInformation("{service}.{method} - finish, get category for user in service layer", nameof(CategoryService), nameof(GetCategoriesForUserAsync));

                return categories;
            }
            else
            {
                _logger.LogError("{service}.{method} - User with id not found!", nameof(CategoryService), nameof(GetCategoriesForUserAsync));
                throw new UserArgumentException("User with id not found!");
            }  
        }
    }
}
