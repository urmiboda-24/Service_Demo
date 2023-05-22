using AutoMapper;
using Service_Demo.Entites.Data;
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
    public class AccountService : GenericService<User>,IAccountService
    {
        private readonly IAccountRepository _accountRepository;

        public AccountService(IAccountRepository accountRepository, IGenericRepository<User> genericRepositoryUser):base(accountRepository)
        {
            _accountRepository = accountRepository;
        }
        public bool EmailAvailable(string email)
        {
            return _accountRepository.AnyData(user => user.Email == email);
        }
        public User PasswordAvailable(LoginViewModel model)
        {
            return _accountRepository.GetFirstOrDefaultData(user => user.Email == model.Email && user.Password == model.Password);
        }

    }
}
