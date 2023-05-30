using AspNetCoreHero.ToastNotification.Abstractions;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Service_Demo.Entites.Models;
using Service_Demo.Models.ViewModels;
using Service_Demo.Services.Interface;

namespace Service_Demo.Controllers
{
    [Authorize(Roles = "User,Admin")]
    public class SkillsController : Controller
    {
        private readonly ISkillService _skillService;
        private readonly INotyfService _toastNotification;
        private readonly IMapper _mapper;
        public SkillsController(ISkillService skillService, INotyfService toastNotification, IMapper mapper)
        {
            _skillService = skillService;
            _toastNotification = toastNotification;
            _mapper = mapper;
        }
        public IActionResult SkillList()
        {
            return View();
        }
        [HttpPost]
        public IActionResult SkillList(SkillsViewModel model)
        {
            bool skillAvailable = _skillService.AnyData(skill => skill.SkillName == model.SkillName.ToLower());
            if (skillAvailable == true && model.Id == 0)
            {
                _toastNotification.Error("Skill already exit", 5);
            }
            else
            {
                var config = new MapperConfiguration(x => x.CreateMap<SkillsViewModel, Skills>());
                var mapper = new Mapper(config);
                if (model.Id == 0)
                {
                    
                    var addSkillMapper = mapper.Map<SkillsViewModel, Skills>(model);
                    _skillService.Add(addSkillMapper);
                    _toastNotification.Success("Skill add successfully", 5);
                }
                else
                {
                    var skill = _skillService.GetFirstOrDefaultData(skill => skill.Id == model.Id);
                    var updatedSkillMapper = mapper.Map(model, skill);
                    updatedSkillMapper.UpdatedAt = DateTime.Now;
                    _skillService.Edit(updatedSkillMapper);
                    _toastNotification.Success("Skill edit successfully", 5);
                }
            }
            return View();
        }

        public IActionResult GetSkillList(string searchText, int pageNumber, string sortBy, int pageSize)
        {
           
            var skills = _skillService.GetSkills(searchText, pageNumber, sortBy, pageSize);
            return PartialView("_SkillTable", skills);
        }
        public IActionResult RemoveSkill(long skillId)
        {
            var skill = _skillService.GetFirstOrDefaultData(skill => skill.Id == skillId);
            skill.DeletedAt = DateTime.Now;
            _skillService.Edit(skill);
            _toastNotification.Success("Skill delete successfully", 5);
            return Ok();
        }
    }
}
