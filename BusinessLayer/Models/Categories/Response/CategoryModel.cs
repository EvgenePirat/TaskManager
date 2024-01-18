using BusinessLayer.DTO.TaskDto.Response;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLayer.Models.Categories.Response
{
    /// <summary>
    /// Model for response category entity
    /// </summary>
    public class CategoryModel
    {
        public Guid Id { get; set; }

        public string Name { get; set; }

        public List<TaskModel>? Tasks { get; set; }
    }
}
