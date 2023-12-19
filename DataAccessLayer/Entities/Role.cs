using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayer.Entities
{
    /// <summary>
    /// Role domain model class for hold data from user
    /// </summary>
    public class Role
    {
        [Key]
        public Guid Id { get; set; }

        [StringLength(20)]
        public string? Name { get; set; }

        public List<User>? Users { get; set; }
    }
}
