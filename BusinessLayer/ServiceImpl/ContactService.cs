using BusinessLayer.Models.Contact.Request;
using BusinessLayer.ServiceContract;
using MailKit.Net.Smtp;
using Microsoft.Extensions.Logging;
using MimeKit;

namespace BusinessLayer.ServiceImpl
{
    /// <summary>
    /// Class for implementation methods for Contact Service
    /// </summary>
    public class ContactService : IContactService
    {
        private readonly string? _systemEmail = "y.y.brekhunchenko@student.khai.edu";
        private readonly string? _systemPassword = "I need think how make for git";
        private readonly int _systemPost = 587;
        private readonly string? _systemHost = "smtp.gmail.com";
        private readonly bool _systemUseSql = false;
        private readonly ILogger<ContactService> _logger;

        public ContactService(ILogger<ContactService> logger)
        {
            _logger = logger;
        }

        public async Task<bool> SendEmailAsync(ContactFormModel contactForm)
        {
            _logger.LogInformation("{service}.{method} - build and post message on email, start", nameof(ContactService), nameof(SendEmailAsync));

            try
            {
                var mensaje = new MimeMessage();
                mensaje.From.Add(new MailboxAddress(contactForm.Name, contactForm.Email));
                mensaje.To.Add(new MailboxAddress("", _systemEmail));
                mensaje.Subject = "Letter from " + contactForm.Name;

                var bodyBuilder = new BodyBuilder();
                bodyBuilder.HtmlBody = contactForm.Message + "\nEmail: " + contactForm.Email;
                mensaje.Body = bodyBuilder.ToMessageBody();

                using (var client = new SmtpClient())
                {
                    await client.ConnectAsync(_systemHost, _systemPost, _systemUseSql);
                    await client.AuthenticateAsync(_systemEmail, _systemPassword);
                    await client.SendAsync(mensaje);
                    await client.DisconnectAsync(true);
                }

                _logger.LogInformation("{service}.{method} - build and post message on email, finish", nameof(ContactService), nameof(SendEmailAsync));

                return true;
            }
            catch (Exception ex)
            {
                _logger.LogError("Error with post message: "+ex.Message);
                return false;
            }
        }
    }
}
