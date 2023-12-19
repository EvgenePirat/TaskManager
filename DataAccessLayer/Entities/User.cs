using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayer.Entities
{
    /// <summary>
    /// Domain model class for hold user dates in basedata
    /// </summary>
    public class User
    {
        public Guid Id { get; set; }

        [StringLength(50)]
        [Required]
        public string? Name { get; set; }

        [StringLength(30)]
        [Required]
        public string? UserName { get; set; }

        [StringLength(50)]
        [Required]
        public string? Password { get; set; }

        [StringLength(50)]
        [Required]
        public string? Email { get; set; }

        public DateTime DateOfBirth { get; set; }

        public Guid RoleId { get; set; }

        public Role? Role { get; set; }

        public int Age { get; set; }

        public DateTime CreateAccount { get; set; }
    }
}
