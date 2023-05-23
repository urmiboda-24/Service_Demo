using Service_Demo.Entites.Models;
using Service_Demo.Models.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service_Demo.Services.Interface
{
    public interface ISkillService : IGenericService<Skills>

    {
        public SkillsViewModel GetSkills(string searchText, int pageNumber);
      /*  public void AddSkill(Skills model);*/
        public void RemoveSkill(long skillId);
        public void AddEditSkill(SkillsViewModel model);
        public bool FindSkillName(SkillsViewModel model);
    }
}
