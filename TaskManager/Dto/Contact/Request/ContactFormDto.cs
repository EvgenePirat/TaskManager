using System.ComponentModel.DataAnnotations;

namespace TaskManager.Dto.Contact.Request
{
    /// <summary>
    /// Contact form dto for hold information about message from user
    /// </summary>
    public class ContactFormDto
    {
        [Required(ErrorMessage = "Name can't be blank")]
        public string? Name { get; set; }

        [Required(ErrorMessage = "Email can't be blank")]
        [EmailAddress(ErrorMessage = "Email should be in a proper email address format")]
        [DataType(DataType.EmailAddress)]
        public string? Email { get; set; }

        [Required(ErrorMessage = "Message can't be blank")]
        public string? Message { get; set; }
    }
}
