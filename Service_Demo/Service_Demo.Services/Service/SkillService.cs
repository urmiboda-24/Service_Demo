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

        public PaginationDataViewModel<SkillsViewModel> GetSkills(string searchText, int pageNumber, string sortBy)
        {

            var skills = _skillRepository.QueryableData(skill => skill.DeletedAt == null).OrderBy(skill => skill.SkillName);
            if (searchText != null)
            {
                skills = _skillRepository.QueryableData(skill => skill.SkillName.ToLower().Contains(searchText.ToLower()) && skill.DeletedAt == null).OrderBy(skill => skill.SkillName);
            }
            if (sortBy == "Status")
            {
                skills = skills.OrderByDescending(skill => skill.Status);
            }
            var result = _skillRepository.GetPageListData(
                    skills.Select(skill => new SkillsViewModel
                    {
                        SkillName = skill.SkillName,
                        Status = skill.Status,
                        Id = skill.Id
                    }),
                    pageNumber
               );
            return result;
        }

        //public void AddEditSkill(SkillsViewModel model)
        //{
        //    var config = new MapperConfiguration(x => x.CreateMap<SkillsViewModel, Skills>());
        //    var mapper = new Mapper(config);


        //    if (model.SkillId == 0)
        //    {
        //        var addSkillMapper = mapper.Map<SkillsViewModel, Skills>(model);
        //        _skillRepository.Add(addSkillMapper);
        //    }
        //    else
        //    {
        //        var skill = _skillRepository.GetFirstOrDefaultData(skill => skill.Id == model.SkillId);
        //        Skills updatedSkillMapper = mapper.Map(model, skill);
        //        updatedSkillMapper.UpdatedAt = DateTime.Now;
        //        _skillRepository.Edit(updatedSkillMapper);
        //    }
        //    _skillRepository.Save();
        //}
    }
}
