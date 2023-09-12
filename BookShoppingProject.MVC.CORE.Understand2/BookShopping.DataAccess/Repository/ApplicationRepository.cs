using BookShopping.DataAccess.Repository.IRepository;
using BookShopping.Models;
using BookShoppingProject.MVC.CORE.Understand2.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookShopping.DataAccess.Repository
{
   public class ApplicationRepository:Repository<ApplicationUser>, IApplicationRepository
    {
        private readonly ApplicationDbContext _context;
        public ApplicationRepository(ApplicationDbContext context):base(context)
        {
            _context = context;
        }
    }
}
