using Service_Demo.Entites.Models;
using Service_Demo.Models.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service_Demo.Services.Interface
{
    public interface IAccountService
    {
        public User UserAvailable(LoginViewModel model);
    }
}
