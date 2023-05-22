﻿using Microsoft.EntityFrameworkCore;
using Service_Demo.Entites.Data;
using Service_Demo.Repository.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Service_Demo.Repository.Repository
{
    public class GenericRepository<T> : IGenericRepository<T> where T : class
    {
        private readonly ServiceDemoContext _context;
        protected DbSet<T> dbset;
        public GenericRepository(ServiceDemoContext context)
        {
            _context = context;
            dbset = _context.Set<T>();
        }

        public IQueryable<T> QueryableData(Expression<Func<T, bool>> condition)
        {
            return _context.Set<T>().Where(condition).AsQueryable();
        }

        public List<T> GetAllData()
        {
            return _context.Set<T>().ToList();
        }

        public List<T> GetFilterData(Expression<Func<T, bool>> condition)
        {
            return _context.Set<T>().Where(condition).ToList();
        }

        public T GetFirstData()
        {
            return _context.Set<T>().First();
        }

        public bool AnyData(Expression<Func<T, bool>> condition)
        {
            return _context.Set<T>().Any(condition);
        }


        public T GetFirstOrDefaultData(Expression<Func<T, bool>> condition)
        {
            return _context.Set<T>().Where(condition).FirstOrDefault();
        }

        public void Save()
        {
            _context.SaveChanges();
        }

        public void Add(T entity)
        {
            _context.Set<T>().Add(entity);
        }

        public void Edit(T entity)
        {
            _context.Set<T>().Update(entity);
        }

        public void Remove(T entity)
        {
            _context.Set<T>().Remove(entity);
        }
    }
}
