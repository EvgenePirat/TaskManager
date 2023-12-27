using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayer.Entities
{
    /// <summary>
    /// Model classes for hold category for task
    /// </summary>
    public class Category
    {
        public Guid Id { get; set; }

        [Required]
        public string? Name { get; set; }

        [Required]
        public DateTime? CreatedDate { get; set; } = DateTime.Now;

        public Guid UserId { get; set; }

        [Required]
        public User? User { get; set; }

        public List<Task>? Tasks { get; set; }
    }
}
