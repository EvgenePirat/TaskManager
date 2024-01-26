using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLayer.Models.Contact.Request
{
    /// <summary>
    /// Contact form model for hold information about message for post
    /// </summary>
    public class ContactFormModel
    {
        public string? Name { get; set; }

        public string? Email { get; set; }

        public string? Message { get; set; }
    }
}
