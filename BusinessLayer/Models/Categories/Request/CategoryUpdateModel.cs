using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLayer.Models.Categories.Request
{
    /// <summary>
    /// Model for update exist category
    /// </summary>
    public class CategoryUpdateModel
    {
        public Guid Id { get; set; }

        public string? OldName { get; set; }

        public string? NewName { get; set; }
    }
}
