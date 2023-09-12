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
   public class OrdeHeaderRepository:Repository<OrderHeader>,IOrderHeaderRepository
    {
        private readonly ApplicationDbContext _context;
        public OrdeHeaderRepository(ApplicationDbContext Context) : base(Context)
        {
            _context = Context;
        }

        public void Update(OrderHeader orderHeader)
        {
            _context.Update(orderHeader);
        }
    }
}
