﻿using BookShoppingProject.DataAccess.Data;
using BookShoppingProject.DataAccess.Repository.IRepository;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace BookShoppingProject.DataAccess.Repository
{
    public class Repository<T> : IRepository<T> where T : class
    {
        private readonly ApplicationDbContext _context;

        internal DbSet<T> dbSet;

        public Repository(ApplicationDbContext context)
        {
            _context = context;

            dbSet = _context.Set<T>();                                                                                                   
        }



        public void Add(T entity)
        {
            dbSet.Add(entity);
        }

        public T FirstOrDefault(Expression<Func<T, bool>> filter = null, string includeProperties = null)
        {
            IQueryable<T> query = dbSet;
            if (filter != null)
                query = query.Where(filter);
            if(includeProperties != null)
                foreach (var includeprop in includeProperties.Split(new char[] {','},StringSplitOptions.RemoveEmptyEntries))
                {
                    query = query.Include(includeprop);
                }
            return query.FirstOrDefault();
        }

        public T Get(int id)
        {
            return dbSet.Find(id);
        }

        public IEnumerable<T> GetAll(Expression<Func<T, bool>> filter = null, Func<IQueryable<T>, IOrderedQueryable<T>> OrderBy = null, string includeProperties = null)
        {
            IQueryable<T> query = dbSet;
            if (filter != null)
                query = query.Where(filter);
            if(includeProperties != null)
            {
                foreach (var includeprop in includeProperties.Split(new char[] {','},StringSplitOptions.RemoveEmptyEntries))
                {
                    query = query.Include(includeprop);
                }
                if (OrderBy != null)
                    return OrderBy(query).ToList();
                
            }
            return query.ToList();
        }

        public void Remove(T entity)
        {
            dbSet.Remove(entity);
        }

        public void Remove(int id)
        {
            var entity = dbSet.Find(id);
            dbSet.Remove(entity);
            
        }

        public void RemoveRange(IEnumerable<T> entity)
        {
            dbSet.RemoveRange(entity);
        }
    }
}
