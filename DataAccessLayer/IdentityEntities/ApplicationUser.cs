using Microsoft.AspNetCore.Identity;

namespace DataAccessLayer.IdentityEntities
{
    /// <summary>
    /// Class for identity user
    /// </summary>
    public class ApplicationUser : IdentityUser<Guid>
    {
        public DateTime DateOfBirth { get; set; }
    }
}
