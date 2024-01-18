using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLayer.Models.Roles.Response
{
    /// <summary>
    /// Model for response role entity
    /// </summary>
    public class RoleModel
    {
        public Guid Id { get; set; }

        public string? Name { get; set; }
    }
}
