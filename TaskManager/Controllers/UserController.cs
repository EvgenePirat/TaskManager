using AutoMapper;
using BusinessLayer.Models.Roles.Response;
using BusinessLayer.Models.Users.Request;
using BusinessLayer.ServiceContract;
using CustomExceptions.UserExceptions;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using TaskManager.Dto.Roles.Response;
using TaskManager.Dto.Users.Request;
using TaskManager.Dto.Users.Response;
using TaskManager.Filteres.ActionFilter.UserFilters;
using TaskManager.Filteres.ErrorFilteres.UserErrorFilteres;

namespace TaskManager.Controllers
{
    /// <summary>
    /// controller for working with user entites
    /// </summary>
    //[Route("[controller]")]
    //public class UserController : Controller
    //{
    //    private readonly IRoleService _roleService;
    //    private readonly IUserService _userService;
    //    private readonly ILogger<UserController> _logger;
    //    private readonly IMapper _mapper;

    //    public UserController(IRoleService roleService, IUserService userService, ILogger<UserController> logger, IMapper mapper)
    //    {
    //        _roleService = roleService;
    //        _userService = userService;
    //        _logger = logger;
    //        _mapper = mapper;
    //    }

    //}
}
