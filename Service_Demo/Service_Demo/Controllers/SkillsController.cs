using AspNetCoreHero.ToastNotification.Abstractions;
using Microsoft.AspNetCore.Mvc;
using Service_Demo.Entites.Models;
using Service_Demo.Models.ViewModels;
using Service_Demo.Services.Interface;

namespace Service_Demo.Controllers
{
    public class SkillsController : Controller
    {
        private readonly ISkillService _skillService;
        private readonly INotyfService _toastNotification;
        public SkillsController(ISkillService skillService, INotyfService toastNotification)
        {
            _skillService = skillService;
            _toastNotification = toastNotification;
        }
        public IActionResult SkillList()
        {
            return View();
        }
        [HttpPost]
        public IActionResult SkillList(SkillsViewModel model)
        {
            bool skillAvailable = _skillService.FindSkillName(model);
            if(skillAvailable == true && model.SkillId == 0)
            {
                _toastNotification.Error("Skill exit", 5);
            }
            else
            {
                bool skillAddOrEdit = _skillService.AddEditSkill(model);
                if (skillAddOrEdit == true)
                {
                    _toastNotification.Success("SKill add successfully", 5);
                }
                else
                {
                    _toastNotification.Success("SKill edit successfully", 5);
                }
            }
            return View();
        }
        public IActionResult GetSkillList(string searchText, int pageNumber)
        {
            var skills = _skillService.GetSkills(searchText, pageNumber);
            return PartialView("_SkillTable", skills);
        }
        public IActionResult RemoveSkill(long skillId)
        {
            _skillService.RemoveSkill(skillId);
            return Ok();
        }
    }
}
