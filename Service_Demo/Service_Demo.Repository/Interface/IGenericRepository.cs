using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Service_Demo.Repository.Interface
{
    public interface IGenericRepository<T>
    {
        public List<T> GetAllData();
        public List<T> GetFilterData(Expression<Func<T, bool>> filter);
        public bool AnyData(Expression<Func<T, bool>> condition);
        public void Save();
        public void Add(T entity);
        public void Edit(T entity);
        public void Remove(T entity);
        public T GetFirstData();
        public T GetFirstOrDefaultData(Expression<Func<T, bool>> condition);
        IQueryable<T> QueryableData(Expression<Func<T, bool>> condition);
    }
}
