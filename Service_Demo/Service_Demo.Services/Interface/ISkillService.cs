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
        public PaginationDataViewModel<SkillsViewModel> GetSkills(string searchText, int pageNumber,string sortBy, int pageSize);
    }
}
