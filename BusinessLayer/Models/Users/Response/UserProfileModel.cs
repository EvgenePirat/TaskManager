using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLayer.Models.Users.Response
{
    /// <summary>
    /// Class model for hold and edit profile user in system
    /// </summary>
    public class UserProfileModel
    {
        public Guid Id { get; set; }

        public string? FirstName { get; set; }

        public string? LastName { get; set; }

        public string? NumberPhone { get; set; }

        public int Age { get; set; }

        public DateTime DateOfBirth { get; set; }

        public string? UserName { get; set; }

        public string? Country { get; set; }

        public string? City { get; set; }

        public bool IsShowWeather { get; set; } = false;

        public string? Email { get; set; }
    }
}
