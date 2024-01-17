using BusinessLayer.DTO.Request;
using BusinessLayer.DTO.Response;
using BusinessLayer.Mapper;
using BusinessLayer.ServiceContract;
using DataAccessLayer.Entities;
using DataAccessLayer.RepositoryContract;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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

        public async Task<CategoryResponse> AddNewCategory(CategoryAddRequest categoryAddRequest)
        {
            if(categoryAddRequest != null)
            {
                Category? category = await _categoryRepository.GetCategoryByNameAsync(categoryAddRequest.Name);

                if (category != null)
                {
                    _logger.LogError("{service}.{method} - Category already exist", nameof(CategoryService), nameof(AddNewCategory));
                    throw new ArgumentException("Category already exist!");
                }

                category = CategoryMapper.CategoryAddRequestToCategory(categoryAddRequest);
                category = await _categoryRepository.AddCategory(category);
                return CategoryMapper.CategoryToCategoryResponse(category);
            }
            else
            {
                _logger.LogError("{service}.{method} - categoryAddRequest equals null", nameof(CategoryService), nameof(AddNewCategory));
                throw new ArgumentNullException(nameof(categoryAddRequest));
            }
        }

        public async Task<List<CategoryResponse>> GetCategoriesForUser(Guid userId)
        {
            if((await _userRepository.GetByUserId(userId)) != null)
            {
                return (await _categoryRepository.GetAllCategories(userId)).Select(temp => CategoryMapper.CategoryToCategoryResponse(temp)).ToList();
            }
            else
            {
                _logger.LogError("{service}.{method} - User with id not found!", nameof(CategoryService), nameof(GetCategoriesForUser));
                throw new ArgumentException(nameof(userId));
            }

            
        }
    }
}
