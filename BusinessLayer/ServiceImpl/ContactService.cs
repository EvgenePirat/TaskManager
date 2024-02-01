using BusinessLayer.Models.Contact.Request;
using BusinessLayer.ServiceContract;
using MailKit.Net.Smtp;
using Microsoft.Extensions.Hosting;
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
        private readonly int _systemPost = 587;
        private readonly string? _systemHost = "smtp.gmail.com";
        private readonly bool _systemUseSql = false;
        private readonly ILogger<ContactService> _logger;
        private readonly IHostEnvironment _env;

        //private const string RELATIVE_PATH = "PersonalData.txt";

        public ContactService(ILogger<ContactService> logger, IHostEnvironment env)
        {
            _logger = logger;
            _env = env;
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

                //string filePath = Path.Combine(_env.ContentRootPath, RELATIVE_PATH);

                //var needDate = File.ReadAllText(filePath);

                using (var client = new SmtpClient())
                {
                    await client.ConnectAsync(_systemHost, _systemPost, _systemUseSql);
                    await client.AuthenticateAsync(_systemEmail, "EvgeneXai2024");
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
