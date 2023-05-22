using Service_Demo.Repository.Interface;
using Service_Demo.Services.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Service_Demo.Services.Service
{
    public class GenericService<T> : IGenericService<T> where T : class
    {
        IGenericRepository<T> _genericRepository;
       
        public GenericService(IGenericRepository<T> genericRepository)
        {
            _genericRepository = genericRepository;
        }
        public void Create(T entity)
        {
            _genericRepository.Add(entity);
        }
        public void Remove(T entity)
        {
            _genericRepository.Remove(entity);
        }
        public void Save()
        {
            _genericRepository.Save();
        }
    }

}



