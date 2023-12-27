using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLayer.DTO.Request
{
    /// <summary>
    /// DTO for interesting a new category
    /// </summary>
    public class CategoryAddRequest
    {
        [Required(ErrorMessage = "Category name can't be blank")]
        public string? Name { get; set; }

        [Required(ErrorMessage = "User id can't be blank")]
        public Guid UserId { get; set; }
    }
}
