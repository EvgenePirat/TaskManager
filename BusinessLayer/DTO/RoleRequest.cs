using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLayer.DTO
{
    /// <summary>
    /// DTO for model user
    /// </summary>
    public class RoleRequest
    {
        [Required(ErrorMessage="Role name can be")]
        public string? Name { get; set; }
    }
}
