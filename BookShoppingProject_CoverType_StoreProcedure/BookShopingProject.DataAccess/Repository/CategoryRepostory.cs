using BookShopingProject.DataAccess.Repository.IRepository;
using BookShopingProject.Models;
using BookShoppingProject_CoverType_StoreProcedure.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookShopingProject.DataAccess.Repository
{
    public class CategoryRepostory : Repository<Category>, ICategoryRepostory
    {
        private readonly ApplicationDbContext _context;
        public CategoryRepostory(ApplicationDbContext context):base(context)
        {
            _context = context;
        }
        public void Update(Category category)
        {
            _context.Update(category);
        }

    }
}
