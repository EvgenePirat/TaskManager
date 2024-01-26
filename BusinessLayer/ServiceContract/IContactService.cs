using BusinessLayer.Models.Contact.Request;

namespace BusinessLayer.ServiceContract
{
    /// <summary>
    /// Rerpesents business logic for post message on email
    /// </summary>
    public interface IContactService
    {
        /// <summary>
        /// Method for post message from client on email
        /// </summary>
        /// <param name="contactForm">data for post on email</param>
        /// <returns>returned true if good or false if error</returns>
        public Task<bool> SendEmailAsync(ContactFormModel contactForm);
    }
}
