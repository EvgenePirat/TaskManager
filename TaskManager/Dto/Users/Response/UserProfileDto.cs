using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;
using TaskManager.Dto.Enums;

namespace TaskManager.Dto.Users.Response
{
    public class UserProfileDto
    {
        [Required(ErrorMessage = "Task id can't be blank")]
        public Guid Id { get; set; }

        public string? FirstName { get; set; }

        public string? LastName { get; set; }

        public string? NumberPhone { get; set; }

        public int Age { get; set; }

        [Required(ErrorMessage = "Date of birth can't be blank")]
        public DateTime DateOfBirth { get; set; }

        [Required(ErrorMessage = "UserName can't be blank")]
        public string? UserName { get; set; }

        public Countries Country { get; set; } = Countries.Unknown;

        public Cities City { get; set; } = Cities.Unknown;

        public bool IsShowWeather { get; set; } = false;

        [Required(ErrorMessage = "Email can't be blank")]
        [EmailAddress(ErrorMessage = "Email should be in a proper email address format")]
        [DataType(DataType.EmailAddress)]
        public string? Email { get; set; }
    }
}
