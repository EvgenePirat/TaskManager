using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLayer.DTO.Response
{
    /// <summary>
    /// DTO for response category entity
    /// </summary>
    public class CategoryResponse
    {
        public Guid Id { get; set; }

        public string? Name { get; set; }

        public DateTime? CreatedDate { get; set; };
    }
}
