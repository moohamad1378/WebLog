using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace EndPoint.Controllers
{
    public class UserController : Controller
    {
        private readonly UserManager<IdentityUser> _manager;
        private readonly SignInManager<IdentityUser> _SignInManager;
        public UserController(UserManager<IdentityUser> userManager,
            SignInManager<IdentityUser> signInManager)
        {
            _manager = userManager;
            _SignInManager = signInManager;
        }
        //[Authorize]
        //[HttpGet]
        //public Task<IActionResult> Index()
        //{
        //    var FindUser = _manager.FindByNameAsync(User.Identity.Name).Result;
        //    return View();
        //}
        //public Task<IActionResult> Register()
        //{
        //    return  ();
        //}  
    }
}
