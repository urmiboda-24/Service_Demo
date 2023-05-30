using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Service_Demo.Services.Interface
{
    public interface IGenericService<T>
    {
       public void Create(T entity);
       public void Save();
       public void Remove(T entity);
       public T GetFirstOrDefaultData(Expression<Func<T, bool>> condition);
       public bool AnyData(Expression<Func<T, bool>> condition);
       public void Edit(T entity);
       public void Add(T entity);
    }
}
