﻿using AutoMapper;
using BusinessLayer.Models.Categories.Request;
using BusinessLayer.Models.Categories.Response;
using BusinessLayer.ServiceContract;
using CustomExceptions.CategoryExceptions;
using CustomExceptions.TaskExceptions;
using CustomExceptions.UserExceptions;
using DataAccessLayer.Entities;
using DataAccessLayer.IdentityEntities;
using DataAccessLayer.RepositoryContract;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Logging;

namespace BusinessLayer.ServiceImpl
{
    /// <summary>
    /// Class for implementation methods for Category Service
    /// </summary>
    public class CategoryService : ICategoryService
    {
        private readonly ICategoryRepository _categoryRepository;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly ILogger<CategoryService> _logger;
        private readonly IMapper _mapper;

        public CategoryService(ICategoryRepository categoryRepository, IUserProfileRepository userRepository, ILogger<CategoryService> logger, IMapper mapper, UserManager<ApplicationUser> userManager)
        {
            _categoryRepository = categoryRepository;
            _userManager = userManager;
            _logger = logger;
            _mapper = mapper;
        }

        public async Task<CategoryModel> AddNewCategoryAsync(CategoryAddModel categoryAddRequest, string? userLogin)
        {
            _logger.LogInformation("{service}.{method} - start, add new category in service layer", nameof(CategoryService), nameof(AddNewCategoryAsync));

            if (userLogin == null)
                throw new ArgumentException("User login equals null");

            if (categoryAddRequest != null)
            {
                if (await CheckNameCategoryForUserAsync(categoryAddRequest.Name, userLogin))
                {
                    _logger.LogError("{service}.{method} - Category already exist", nameof(CategoryService), nameof(AddNewCategoryAsync));
                    throw new CategoryArgumentException("Category already exist!");
                }

                var user = await _userManager.FindByNameAsync(userLogin);

                var mappedCategory = _mapper.Map<Category>(categoryAddRequest);
                mappedCategory.UserId = user.Id;
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

        private async Task<bool> CheckNameCategoryForUserAsync(string categoryName, string? userLogin)
        {
            var categories = await GetCategoriesForUserAsync(userLogin);
            CategoryModel? categoryFind = categories.FirstOrDefault(temp =>  temp.Name == categoryName);
            return categoryFind != null;
        }

        public async Task<List<CategoryModel>> GetCategoriesForUserAsync(string? userLogin)
        {
            _logger.LogInformation("{service}.{method} - start, get category for user in service layer", nameof(CategoryService), nameof(GetCategoriesForUserAsync));

            if (userLogin == null)
                throw new ArgumentException("User login equals null");

            var user = await _userManager.FindByNameAsync(userLogin);

            if (user != null)
            {
                var categories = _mapper.Map<List<CategoryModel>>(await _categoryRepository.GetAllCategoriesAsync(user.Id));

                _logger.LogInformation("{service}.{method} - finish, get category for user in service layer", nameof(CategoryService), nameof(GetCategoriesForUserAsync));

                return categories;
            }
            else
            {
                _logger.LogError("{service}.{method} - User with login not found!", nameof(CategoryService), nameof(GetCategoriesForUserAsync));
                throw new UserArgumentException("User with login not found!");
            }  
        }

        public async System.Threading.Tasks.Task DeleteByIdAsync(Guid categoryId)
        {
            _logger.LogInformation("{service}.{method} - start, delete category in service layer", nameof(CategoryService), nameof(DeleteByIdAsync));

            if(await _categoryRepository.GetCategoryByIdAsync(categoryId) != null)
            {
                await _categoryRepository.DeleteCategoryByIdAsync(categoryId);

                _logger.LogInformation("{service}.{method} - finish, delete category in service layer", nameof(CategoryService), nameof(DeleteByIdAsync));
            }
            else
            {
                _logger.LogError("{service}.{method} - not found category for delete", nameof(CategoryService), nameof(DeleteByIdAsync));
                throw new CategoryArgumentException("category not found for delete");
            }
        }

        public async Task<CategoryModel> UpdateCategoryAsync(CategoryUpdateModel categoryUpdateModel)
        {
            _logger.LogInformation("{service}.{method} - start, update category in service layer", nameof(CategoryService), nameof(UpdateCategoryAsync));

            var categoryFromBD = await _categoryRepository.GetCategoryByIdAsync(categoryUpdateModel.Id);
            if (categoryFromBD != null && categoryFromBD.Name == categoryUpdateModel.OldName)
            {
                var mappedCategory = _mapper.Map<Category>(categoryUpdateModel);
                mappedCategory.Name = categoryUpdateModel.NewName;
                var result = await _categoryRepository.UpdateCategoryAsync(mappedCategory);

                _logger.LogInformation("{service}.{method} - finish, update category in service layer", nameof(CategoryUpdateModel), nameof(UpdateCategoryAsync));

                return _mapper.Map<CategoryModel>(result);
            }
            else
            {
                _logger.LogError("{service}.{method} - not found category for update", nameof(CategoryService), nameof(UpdateCategoryAsync));
                throw new TaskArgumentException("Category not found for update");
            }
        }
    }
}
