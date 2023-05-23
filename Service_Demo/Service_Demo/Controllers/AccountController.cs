using AspNetCoreHero.ToastNotification.Abstractions;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Service_Demo.Auth;
using Service_Demo.Entites;
using Service_Demo.Entites.Auth;
using Service_Demo.Entites.Models;
using Service_Demo.Models.ViewModels;
using Service_Demo.Services.Interface;


namespace Service_Demo.Controllers
{
    public class AccountController : Controller
    {
        private readonly IAccountService _accountService;
        private readonly INotyfService _toastNotification;
        private readonly IConfiguration _configuration;
        private readonly IGenericService<User> _genericService;
        public AccountController(IAccountService accountService, INotyfService toastNotification, IConfiguration configuration, IGenericService<User> genericService)
        {
            _accountService = accountService;
            _toastNotification = toastNotification;
            _configuration = configuration;
            _genericService = genericService;
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
                    var userdValid = _accountService.UserAvailable(model);
                    {
                        if (userdValid != null)
                        {
                            var user = _genericService.GetFirstOrDefaultData(user => user.Email == model.Email);
                            var config = new MapperConfiguration(x => x.CreateMap<User, SessionDetailsViewModel>().ForMember(dest => dest.FullName, opt => opt.MapFrom(src => $"{src.FirstName} {src.LastName}")));

                            var mapper = new Mapper(config);
                            var sessionDetailsViewModel = mapper.Map<User, SessionDetailsViewModel>(user);

                            var jwtSetting = _configuration.GetSection(nameof(JwtSetting)).Get<JwtSetting>();

                            var token = JwtTokenHelper.GenerateToken(jwtSetting, sessionDetailsViewModel);

                            if (string.IsNullOrWhiteSpace(token))
                            {
                                _toastNotification.Error("User not exits", 5);
                            }
                            HttpContext.Session.SetString("UserID", (userdValid.Id).ToString());
                            HttpContext.Session.SetString("Email", userdValid.Email);
                            HttpContext.Session.SetString("Token", token);
                            HttpContext.Session.SetString("Avatar", userdValid.Avatar);
                            HttpContext.Session.SetString("Role", userdValid.Role);
                            HttpContext.Session.SetString("Name", userdValid.FirstName + " " + userdValid.LastName);

                            if(userdValid.Role.ToLower() =="admin")
                            {
                                _toastNotification.Success("Admin Login successfully");
                                return RedirectToAction("SkillList", "Skills");
                            }
                            else
                            {
                                _toastNotification.Success("User Login successfully");
                                return RedirectToAction("SkillList", "Skills");
                            }
                           
                        }
                        else
                        {

                            _toastNotification.Error("Wrong password", 5);
                        }
                    }
            }
            return View();
        }
        public IActionResult Logout()
        {
            if (!string.IsNullOrEmpty(HttpContext.Session.GetString("Email")))
            {
                HttpContext.Session.Clear();
            }
            return RedirectToAction("Login", "Account");
        }
    }
}
