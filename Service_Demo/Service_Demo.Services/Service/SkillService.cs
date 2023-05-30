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

        public PaginationDataViewModel<SkillsViewModel> GetSkills(string searchText, int pageNumber, string sortBy, int pageSize)
        {
            var skills = (searchText != null) ? _skillRepository.QueryableData(skill => skill.SkillName.ToLower().Contains(searchText.ToLower()) && skill.DeletedAt == null) :
                         _skillRepository.QueryableData(skill => skill.DeletedAt == null);
            if (sortBy == "Status")
            {
                skills = skills.OrderByDescending(skill => skill.Status);
            }
            else if(sortBy == "SkillName")
            {
                skills = skills.OrderBy(skill => skill.SkillName);
            }
            else
            {
                skills = skills.OrderByDescending(skill => skill.CreatedAt);
            }
            var result = _skillRepository.GetPageListData(
                    skills.Select(skill => new SkillsViewModel
                    {
                        SkillName = skill.SkillName,
                        Status = skill.Status,
                        Id = skill.Id
                    }),
                    pageNumber,
                    pageSize
               );
            return result;
        }

    }
}
