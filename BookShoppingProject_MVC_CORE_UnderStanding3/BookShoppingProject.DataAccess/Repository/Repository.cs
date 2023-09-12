using BookShoppingProject.DataAccess.Repository.IRepository;
using BookShoppingProject_MVC_CORE_UnderStanding3.Data;
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
       internal DbSet<T> Dbset;
        public Repository(ApplicationDbContext context)
        {
            _context = context;
            Dbset = _context.Set<T>();
        }
        public void Add(T entity)
        {
            Dbset.Add(entity);
        }

        public T FirstOrDefault(Expression<Func<T, bool>> filter = null, string includeProperties = null)
        {
            IQueryable<T> query = Dbset;
            if (filter != null)
                query = query.Where(filter);
            if(includeProperties !=null)
            {
                foreach (var includeProp in includeProperties.Split(new char[] {','},StringSplitOptions.RemoveEmptyEntries))
                {
                    query = query.Include(includeProp);
                }
            }
            return query.FirstOrDefault();
        }

        public T Get(int id)
        {
            return Dbset.Find(id);
        }

        public IEnumerable<T> GetAll(Expression<Func<T, bool>> filter = null, Func<IQueryable<T>, IOrderedQueryable<T>> orderBy = null, string includeProperties = null)
        {
            IQueryable<T> query = Dbset;
            if (filter != null)
                return query.Where(filter);
            if(includeProperties !=null)
            {
                foreach (var includeProp in includeProperties.Split(new char[] {','},StringSplitOptions.RemoveEmptyEntries))
                {
                    query = query.Include(includeProp);
                }
                if (orderBy != null)
                    return orderBy(query).ToList();
            }
            return query.ToList();
        }

        public void Remove(T entity)
        {
            Dbset.Remove(entity);
        }

        public void Remove(int id)
        {
            var entity = Dbset.Find(id);
            Dbset.Remove(entity);
        }

        public void RemoveRange(IEnumerable<T> entity)
        {
            Dbset.RemoveRange(entity);
            
        }
    }
    
}
