using Service_Demo.Entites.Models;
using Service_Demo.Models.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service_Demo.Repository.Interface
{
    public interface IAccountRepository:IGenericRepository<User>
    {
    }
}
