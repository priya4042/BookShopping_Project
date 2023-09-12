using BookShoppingProject.DataAccess.Data;
using BookShoppingProject.DataAccess.Repository.IRepository;
using BookShoppingProject.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookShoppingProject.DataAccess.Repository
{
    public class CategoryRepository : Repository<Category>, ICategoryRepository
    {
        private readonly ApplicationDbContext _Context;
        public CategoryRepository(ApplicationDbContext context):base(context)
        {
            _Context = context;
        }
        public void Update(Category category)
        {
            _Context.Update(category);
        }
    }
}
