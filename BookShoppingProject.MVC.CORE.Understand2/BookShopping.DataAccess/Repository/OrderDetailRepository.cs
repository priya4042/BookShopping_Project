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
   public class OrderDetailRepository:Repository<OrderDetails>,IOrderDetailRepository
    {
        private readonly ApplicationDbContext _context;
        public OrderDetailRepository(ApplicationDbContext Context) : base(Context)
        {
            _context = Context;
        }

        public void Update(OrderDetails orderDetails)
        {
            _context.Update(orderDetails);
        }
    }
}
