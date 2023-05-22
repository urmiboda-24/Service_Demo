using Service_Demo.Entites.Data;
using Service_Demo.Entites.Models;
using Service_Demo.Repository.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service_Demo.Repository.Repository
{
    public class SkillRepository : GenericRepository<Skills> , ISkillRepository
    {
        private readonly ServiceDemoContext _context;

        public SkillRepository(ServiceDemoContext context) : base(context)
        {
            _context = context;
        }
    }
}
