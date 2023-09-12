using BookShopping.revise.Data;
using BookShopping_DataAccess.Repository.IRepository;
using BookShopping_Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookShopping_DataAccess.Repository
{
   public class ApplicationUserRepositore:Repository<ApplicationUser>, IApplicationUserRepositore
    {
        private readonly ApplicationDbContext _context;
        public ApplicationUserRepositore(ApplicationDbContext context):base(context)
        {
            _context = context;
        }
    }
}
