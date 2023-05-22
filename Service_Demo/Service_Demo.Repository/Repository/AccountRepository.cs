using Service_Demo.Entites.Data;
using Service_Demo.Entites.Models;
using Service_Demo.Models.ViewModels;
using Service_Demo.Repository.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service_Demo.Repository.Repository
{
    public class AccountRepository : GenericRepository<User> ,IAccountRepository
    {
        private readonly ServiceDemoContext _context;

        public AccountRepository(ServiceDemoContext context):base(context)
        {
            _context = context;
        }

        
    }
}
