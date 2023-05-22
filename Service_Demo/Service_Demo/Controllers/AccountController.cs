using AspNetCoreHero.ToastNotification.Abstractions;
using Microsoft.AspNetCore.Mvc;
using Service_Demo.Auth;
using Service_Demo.Entites;
using Service_Demo.Entites.Auth;
using Service_Demo.Models.ViewModels;
using Service_Demo.Services.Interface;


namespace Service_Demo.Controllers
{
    public class AccountController : Controller
    {
        private readonly IAccountService _accountService;
        private readonly INotyfService _toastNotification;
        private readonly IConfiguration _configuration;
        public AccountController(IAccountService accountService, INotyfService toastNotification, IConfiguration configuration)
        {
            _accountService = accountService;
            _toastNotification = toastNotification;
            _configuration = configuration;
        }
        public IActionResult Login()
        {
            return View();
        }
        [HttpPost]
        public IActionResult Login(LoginViewModel model)
        {
            if (ModelState.IsValid)
            {

                bool emailValid = _accountService.EmailAvailable(model.Email);
                if (emailValid)
                {
                    var passwordValid = _accountService.PasswordAvailable(model);
                    {
                        if (passwordValid != null)
                        {
                            SessionDetailsViewModel sessionDetailsViewModel = new SessionDetailsViewModel();
                            sessionDetailsViewModel.Email = passwordValid.Email;
                            sessionDetailsViewModel.Avatar = passwordValid.Avatar;
                            sessionDetailsViewModel.UserId = passwordValid.Id;
                            sessionDetailsViewModel.FullName = passwordValid.FirstName + " " + passwordValid.LastName;
                            sessionDetailsViewModel.Role = passwordValid.Role;

                            var jwtSetting = _configuration.GetSection(nameof(JwtSetting)).Get<JwtSetting>();

                            var token = JwtTokenHelper.GenerateToken(jwtSetting, sessionDetailsViewModel);

                            if (string.IsNullOrWhiteSpace(token))
                            {
                                ModelState.AddModelError("email", "Enter correct email");
                                _toastNotification.Error("User not exits", 5);
                                return View("Login");
                            }
                            HttpContext.Session.SetString("UserID", (passwordValid.Id).ToString());
                            HttpContext.Session.SetString("Email", passwordValid.Email);
                            HttpContext.Session.SetString("Token", token);
                            HttpContext.Session.SetString("Avatar", passwordValid.Avatar);
                            HttpContext.Session.SetString("Name", passwordValid.FirstName + " " + passwordValid.LastName);

                            if(passwordValid.Role.ToLower() =="admin")
                            {
                                _toastNotification.Success("Admin Login successfully");
                                return RedirectToAction("SkillList", "Skills");
                            }
                            else
                            {
                                _toastNotification.Success("User Login successfully");
                                return RedirectToAction("Index", "Home");
                            }
                           
                        }
                        else
                        {

                            _toastNotification.Error("Wrong password", 5);
                        }
                    }
                }
                else
                {
                    _toastNotification.Error("User not exits", 5);
                }
            }
            return View();
        }
    }
}
