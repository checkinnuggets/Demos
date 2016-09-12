using System;
using System.Security.Claims;
using System.Web;
using System.Web.Mvc;
using ShortlistManager.Services;
using ShortlistManager.Services.Models;
using ShortlistManager.Web.Models;
using ShortlistManager.Web.Infrastructure.Auth;
using ShortlistManager.Web.Infrastructure.Filters.ModelState;

namespace ShortlistManager.Web.Controllers
{
    [AllowAnonymous]
    public class AuthController : Controller
    {
        private readonly IAuthService _authService;

        private const string AuthenticationType = "ApplicationCookie";

        public AuthController(IAuthService authService)
        {
            _authService = authService;
        }


        [HttpGet]
        [RetrieveModelState]
        public ActionResult LogIn()
        {
            var model = new LogInModel();
            return View(model);
        }


        [HttpPost]
        [PersistModelState]
        public ActionResult LogIn(LogInModel model)
        {
            if (!ModelState.IsValid)
            {
                return RedirectToAction("LogIn");
            }

            UserDto user;
            if (!_authService.TryAuthenticate(model.UserName, model.Password, out user))
            {
                // this will never happen in this demo
                throw new Exception("Login failed.");
            }


            var identity = new ClaimsIdentity(new[] {
                new Claim(AuthHelpers.UserNameClaimKey, user.UserName),
                new Claim(AuthHelpers.UserIdClaimKey, user.Id.ToString())
            }, AuthenticationType);



            var authenticationManager = Request.GetOwinContext().Authentication;
            authenticationManager.SignIn(identity);

            return RedirectToAction("Index", "Spa");
        }

        [HttpGet]
        public ActionResult LogOut()
        {
            var authenticationManager = Request.GetOwinContext().Authentication;
            authenticationManager.SignOut(AuthenticationType);
            return RedirectToAction("Index", "Home");
        }

    }
}