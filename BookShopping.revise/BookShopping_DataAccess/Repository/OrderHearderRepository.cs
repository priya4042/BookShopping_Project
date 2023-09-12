using BookShopping_Models;
using BookShopping.revise.Data;
using BookShopping_DataAccess.Repository.IRepository;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookShopping_DataAccess.Repository
{
    public class OrderHearderRepository : Repository<OrderHeader>, IOrderHeaderRepository
    {
        private readonly ApplicationDbContext _context;
        public OrderHearderRepository(ApplicationDbContext Context):base(Context)
        {
            _context = Context;
        }
        public void Update(OrderHeader orderHeader)
        {
            _context.Update(orderHeader);
        }
    }
}
