using Service_Demo.Entites.Models;
using Service_Demo.Models.ViewModels;
using Service_Demo.Repository.Interface;
using Service_Demo.Services.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service_Demo.Services.Service
{
    public class SkillService :GenericService<Skills>, ISkillService
    {
        private readonly ISkillRepository _skillRepository;


        public SkillService(ISkillRepository skillRepository) : base(skillRepository)
        {
            _skillRepository = skillRepository;
        }
        public SkillsViewModel GetSkills(string searchText, int pageNumber)
        {
            if (pageNumber == 0)
            {
                pageNumber = 1;
            }
            int pagesize = 1;
            var skills = _skillRepository.QueryableData(skill => skill.DeletedAt == null);
            if (searchText != null)
            {
                skills = _skillRepository.QueryableData(skill => skill.SkillName.ToLower().Contains(searchText.ToLower()) && skill.DeletedAt == null);
            }
            int pagecount = (int)Math.Ceiling((double)skills.Count() / pagesize);
            var skills1 = skills.Skip((pageNumber - 1) * pagesize).Take(pagesize);
            SkillsViewModel skillsViewModel = new SkillsViewModel();
            skillsViewModel.Skills = skills1.ToList();

            skillsViewModel.PageSize = pagesize;
            skillsViewModel.PageCount = pagecount;
            skillsViewModel.CurrentPage = pageNumber;

            return skillsViewModel;
        }
        /*public void AddSkill(Skills model)
        {
            _skillRepository.Add(model);
            _skillRepository.Save();
        }*/
        public void RemoveSkill(long skillId)
        {
            var skill = _skillRepository.GetFirstOrDefaultData(skill => skill.Id == skillId);
            skill.DeletedAt = DateTime.Now;
            _skillRepository.Edit(skill);
            _skillRepository.Save();
        }
        public bool FindSkillName(SkillsViewModel model)
        {
            return _skillRepository.AnyData(skill => skill.SkillName == model.SkillsName);
        }
        public bool AddEditSkill(SkillsViewModel model)
        {
            var skill = _skillRepository.GetFirstOrDefaultData(skill => skill.Id == model.SkillId);
            if(model.SkillId == 0 && skill == null)
            {
                Skills skills = new Skills();
                skills.Status = (bool)model.Status;
                skills.SkillName = model.SkillsName;
                _skillRepository.Add(skills);
                _skillRepository.Save();
                return true;
            }
            else
            {
                skill.UpdatedAt = DateTime.Now;
                skill.SkillName = model.SkillsName;
                skill.Status = (bool)model.Status;
                _skillRepository.Edit(skill);
                _skillRepository.Save();
                return false;
            }
           
        }
    }
}
