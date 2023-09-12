
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
    public class OrderDetailrePository : Repository<OrderDetails>, IOrderDetailRepository
    {
        private readonly ApplicationDbContext _context;
        public OrderDetailrePository(ApplicationDbContext context):base(context)
        {
            _context = context;
        }
        public void Update(OrderDetails orderDetail)
        {
            _context.Update(orderDetail);
        }
    }
}
