﻿using LeLeInstitute.DAL;
using LeLeInstitute.Services.IRepository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LeLeInstitute.Services.Repository
{
    public class Repository<T> : IRepository<T> where T : class
    {
        protected readonly LeLeContext LeLeContext;
        public Repository(LeLeContext leLeContext)
        {
            LeLeContext = leLeContext;
                    
        }
        private void Save() => LeLeContext.SaveChanges(); 
        public void Add(T entity)
        {
            LeLeContext.Add(entity);
            Save();
        }

        public int Count(Func<T, bool> predicate)
        {
            return LeLeContext.Set<T>().Where(predicate).Count();
        }

        public void Delete(T entity)
        {
            LeLeContext.Remove(entity);
            Save();
        }

        public IEnumerable<T> GetAll()
        {
            return LeLeContext.Set<T>().ToList();
        }

        public IEnumerable<T> GetByFiler(Func<T, bool> predicate)
        {
            return LeLeContext.Set<T>().Where(predicate).ToList();
        }

        public T GetById(int id)
        {
            return LeLeContext.Set<T>().Find(id);
        }

        public void Update(T entity)
        {
            LeLeContext.Entry(entity).State = Microsoft.EntityFrameworkCore.EntityState.Modified;
            Save();
        }
    } 
}
