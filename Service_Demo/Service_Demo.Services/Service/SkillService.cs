using AutoMapper;
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
        private readonly IMapper _mapper;


        public SkillService(ISkillRepository skillRepository, IMapper mapper) : base(skillRepository)
        {
            _skillRepository = skillRepository;
            _mapper = mapper;
        }
        public SkillsViewModel GetSkills(string searchText, int pageNumber)
        {
            if (pageNumber == 0)
            {
                pageNumber = 1;
            }
            int pagesize = 3;
            var skills = _skillRepository.QueryableData(skill => skill.DeletedAt == null).OrderBy(skill => skill.SkillName); 
            if (searchText != null)
            {
                skills = _skillRepository.QueryableData(skill => skill.SkillName.ToLower().Contains(searchText.ToLower()) && skill.DeletedAt == null).OrderBy(skill => skill.SkillName);
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
        public void RemoveSkill(long skillId)
        {
            var skill = _skillRepository.GetFirstOrDefaultData(skill => skill.Id == skillId);
            skill.DeletedAt = DateTime.Now;
            _skillRepository.Edit(skill);
            _skillRepository.Save();
        }
        public bool FindSkillName(SkillsViewModel model)
        {
            return _skillRepository.AnyData(skill => skill.SkillName == model.SkillName);
        }
        public void AddEditSkill(SkillsViewModel model)
        {
            var config = new MapperConfiguration(x => x.CreateMap<SkillsViewModel, Skills>());
            var mapper = new Mapper(config);


            if (model.SkillId == 0)
            {
                var addSkillMapper = mapper.Map<SkillsViewModel, Skills>(model);
                _skillRepository.Add(addSkillMapper);
            }
            else
            {
                var skill = _skillRepository.GetFirstOrDefaultData(skill => skill.Id == model.SkillId);
                Skills updatedSkillMapper = mapper.Map(model, skill);
                _skillRepository.Edit(updatedSkillMapper);
            }
            _skillRepository.Save();
        }
    }
}
