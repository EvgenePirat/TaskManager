using AutoMapper;
using BusinessLayer.Models.Contact.Request;
using BusinessLayer.ServiceContract;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using TaskManager.Dto.Contact.Request;

namespace TaskManager.Controllers
{
    /// <summary>
    /// Class for working with feedback about application from client
    /// </summary>
    [Route("[controller]")]
    [Authorize(Roles = "User")]
    public class ContactController : Controller
    {
        private readonly ILogger<ContactController> _logger;
        private readonly IContactService _contactService;
        private readonly IMapper _mapper;

        public ContactController(ILogger<ContactController> logger, IContactService contactService, IMapper mapper)
        {
            _logger = logger;
            _contactService = contactService;
            _mapper = mapper;
        }

        /// <summary>
        /// Method for get page with form for post email message
        /// </summary>
        /// <returns>returned page for feedback</returns>
        [HttpGet("[action]")]
        public ActionResult Feedback()
        {
            _logger.LogInformation("{controller}.{method} - Get contact page", nameof(ContactController), nameof(Feedback));

            return View();
        }

        /// <summary>
        /// Method for post message on email
        /// </summary>
        /// <param name="contactFormDto">data about feedback from user</param>
        /// <returns>returned good message if post or error</returns>
        [HttpPost("[action]")]
        public async Task<IActionResult> FeedbackPost(ContactFormDto contactFormDto)
        {
            _logger.LogInformation("{controller}.{method} - start, post message on email from client", nameof(ContactController), nameof(FeedbackPost));

            var mappedModel = _mapper.Map<ContactFormModel>(contactFormDto);

            var result = await _contactService.SendEmailAsync(mappedModel);

            if (result)
            {
                ViewBag.Result = result;
                ViewBag.ResultMessage = "Message sent successfully!";
            }
            else
            {
                ViewBag.Result = result;
                ViewBag.ResultMessage = "We got error. Try later!";
            }
               
            _logger.LogInformation("{controller}.{method} - finish, post message on email from client", nameof(ContactController), nameof(FeedbackPost));

            return View("Feedback");
        }
    }
}
