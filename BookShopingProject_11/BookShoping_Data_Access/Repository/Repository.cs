using BookShoping_Data_Access.Repository.IRepository;
using BookShopingProject.Data_Access.Data;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace BookShoping_Data_Access.Repository
{
    public class Repository<T> : IRepository<T> where T : class
    {
        private readonly ApplicationDbContext _context;
        internal DbSet<T> dbset;

        public Repository(ApplicationDbContext context)
        {
            _context = context;
            dbset = _context.Set<T>();
        }


        public void Add(T entity)
        {
            dbset.Add(entity);
        }

        public T FirstOrDefault(Expression<Func<T, bool>> filter = null, string includeProperties = null)
        {
            IQueryable<T> query = dbset;
            if (filter != null)
                query = query.Where(filter);
            if (includeProperties != null)
            {
                foreach (var includeProp in includeProperties.Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries))
                {
                    query = query.Include(includeProp);
                }
                
            }
            return query.FirstOrDefault();
        }

        public T Get(int id)
        {
            return dbset.Find(id);
        }

        public IEnumerable<T> GetAll(Expression<Func<T, bool>> Filter = null, Func<IQueryable<T>, IOrderedQueryable<T>> OrderBy = null, string includeProperties = null)
        {
            IQueryable<T> query = dbset;
            if (Filter != null)
                query = query.Where(Filter);
            if (includeProperties != null)
            {
                foreach (var includeProp in includeProperties.Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries))
                {
                    query = query.Include(includeProp);
                }

                if (OrderBy != null)
                    return OrderBy(query).ToList();
            }
            return query.ToList();

        }

        public void Remove(T entity)
        {
            dbset.Remove(entity);
        }

        public void Remove(int id)
        {
            var entity = dbset.Find(id);
            //dbset.Remove(entity);
            //Remove(entity);
           // var entity = Get(id);
        }

        public void RemoveRange(IEnumerable<T> entity)
        {
            dbset.RemoveRange(entity);
        }
    }


}
