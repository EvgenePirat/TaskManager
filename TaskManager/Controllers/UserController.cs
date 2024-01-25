

using AutoMapper;
using BusinessLayer.ServiceContract;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Mvc;

namespace TaskManager.Controllers
{
    /// <summary>
    /// controller for working with user entites
    /// </summary>
    [Microsoft.AspNetCore.Mvc.Route("[controller]")]
    public class UserController : Controller
    {
        private readonly IUserService _userService;
        private readonly ILogger<UserController> _logger;
        private readonly IMapper _mapper;

        public UserController(IUserService userService, ILogger<UserController> logger, IMapper mapper)
        {
            _userService = userService;
            _logger = logger;
            _mapper = mapper;
        }

    }
}
