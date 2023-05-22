﻿using System;
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
    }
}
