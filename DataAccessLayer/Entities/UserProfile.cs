using DataAccessLayer.IdentityEntities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayer.Entities
{
    /// <summary>
    /// Domain model class for hold user dates in basedata
    /// </summary>
    [Table("UserProfile")]
    public class UserProfile
    {
        [Key]
        public Guid UserProfileId { get; set; }

        [StringLength(50)]
        public string? FirstName { get; set; }


        [StringLength(50)]
        public string? LastName { get; set; }

        [StringLength(30)]
        public string? NumberPhone { get; set; }

        public int Age { get; set; }

        public DateTime CreateAccount { get; set; } = DateTime.Now;

        [Required]
        public ApplicationUser? ApplicationUser { get; set; }
    }
}
