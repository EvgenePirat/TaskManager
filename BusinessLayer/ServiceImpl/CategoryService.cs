using BusinessLayer.DTO.Request;
using BusinessLayer.DTO.Response;
using BusinessLayer.Mapper;
using BusinessLayer.ServiceContract;
using DataAccessLayer.Entities;
using DataAccessLayer.RepositoryContract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLayer.ServiceImpl
{
    public class CategoryService : ICategoryService
    {
        private readonly ICategoryRepository _categoryRepository;

        private readonly IUserRepository _userRepository;

        public CategoryService(ICategoryRepository categoryRepository, IUserRepository userRepository)
        {
            _categoryRepository = categoryRepository;
            _userRepository = userRepository;
        }

        public async Task<CategoryResponse> AddNewCategory(CategoryAddRequest categoryAddRequest)
        {
            Category category = CategoryMapper.CategoryAddRequestToCategory(categoryAddRequest);
            category = await _categoryRepository.AddCategory(category);
            return CategoryMapper.CategoryToCategoryResponse(category);
        }

        public Task<List<CategoryResponse>> GetCategoriesForUser(Guid userId)
        {
            throw new NotImplementedException();
        }
    }
}
