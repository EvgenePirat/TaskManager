using BusinessLayer.DTO.Request;
using BusinessLayer.DTO.Response;
using DataAccessLayer.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLayer.Mapper
{
    /// <summary>
    /// Class mapper allows change class on other class with save data for category entity
    /// </summary>
    public static class CategoryMapper
    {
        /// <summary>
        /// Method with logic for change from categoryaddrequest class to category class for save in bd
        /// </summary>
        /// <param name="categoryAddRequest">categoryaddrequest has data for category</param>
        /// <returns>returned category with data from categoryaddrequest</returns>
        public static Category CategoryAddRequestToCategory(CategoryAddRequest categoryAddRequest)
        {
            return new Category() { Name = categoryAddRequest.Name, UserId = categoryAddRequest.UserId };
        }

        /// <summary>
        /// Method with logic for change from category class to categoryResponse class
        /// </summary>
        /// <param name="category">category has data for categoryResponse</param>
        /// <returns>returned categoryResponse with data from category</returns>
        public static CategoryResponse CategoryToCategoryResponse(Category category)
        {
            return new CategoryResponse() { Id = category.Id, Name = category.Name, CreatedDate = category.CreatedDate, Tasks = category.Tasks?.Select(task => TaskMapper.TaskToTaskResponse(task)).ToList() };
        }
    }
}
