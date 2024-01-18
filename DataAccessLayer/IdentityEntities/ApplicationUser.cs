using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
