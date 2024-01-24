using DataAccessLayer.Entities;
using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;

namespace DataAccessLayer.IdentityEntities
{
    /// <summary>
    /// Class for identity user
    /// </summary>
    public class ApplicationUser : IdentityUser<Guid>
    {
        public DateTime DateOfBirth { get; set; }

        [Required]
        public UserProfile? UserProfile { get; set; }

        public List<Category>? Categories { get; set; }

    }
}
