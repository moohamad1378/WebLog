using Application.CQRS.UsersCQRS.Commands;
using Application.CQRS.UsersCQRS.Queries;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace EndPoint.Controllers
{
    public class UserController : Controller
    {
        private readonly UserManager<IdentityUser> _manager;
        private readonly SignInManager<IdentityUser> _SignInManager;
        private readonly IMediator mediator;

        public UserController(UserManager<IdentityUser> userManager,
            SignInManager<IdentityUser> signInManager,IMediator mediator)
        {
            _manager = userManager;
            _SignInManager = signInManager;
            this.mediator = mediator;
        }
        [Authorize]
        [HttpGet]
        public async Task<IActionResult> Index()
        {
            var FindUser = _manager.FindByNameAsync(User.Identity.Name).Result;
            return View();
        }
        public async Task<IActionResult> Register(AddUserCommandRequestDto addUserCommandResponsDto)
        {
            AddUserCommand addUserCommand1=new AddUserCommand(addUserCommandResponsDto);
            var result= mediator.Send(addUserCommand1).Result;
            return View();
        }
        [Authorize]
        [HttpGet]
        public async Task<IActionResult> Edit(GetUserQueryHandlerDto getUserQueryHandlerDto)
        {
            GetUserQuery getUserQuery=new GetUserQuery(getUserQueryHandlerDto);
            var resutl = mediator.Send(getUserQuery).Result;
            return View(resutl);
        }
    }
}
