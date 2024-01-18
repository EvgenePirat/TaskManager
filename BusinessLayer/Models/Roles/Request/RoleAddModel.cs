using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLayer.Models.Roles.Request
{
    /// <summary>
    /// Model for add new role entiti
    /// </summary>
    public class RoleAddModel
    {
        public string? Name { get; set; }
    }
}
