using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
//LINQ--(Language integrated query) makes the code more readable
//so other developers can easily understand and maintain it. 
namespace Bookshopping.DataAccess.Repository.IRepository
{
   public interface IRepository<T> where T: class
    {
        T Get(int id);//Find --it give one table result and work with primary key column

        IEnumerable<T> GetAll(Expression<Func<T, bool>> filter=null,

           Func<IQueryable<T>,IOrderedQueryable<T>> OrderBy=null,

           string includeProperties=null
            
            );
        T FirstOrDefault(Expression<Func<T, bool>> filter = null,

            string includeProperties = null

            );
        void Add(T entity);
        void Remove(T entity);
        void Remove(int id);
        void RemoveRange(IEnumerable<T> entity);
    }
}
