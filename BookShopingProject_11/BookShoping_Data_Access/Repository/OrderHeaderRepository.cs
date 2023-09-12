using BookShoping_Data_Access.Repository.IRepository;
using BookShoping_Models;
using BookShopingProject.Data_Access.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookShoping_Data_Access.Repository
{
    public class OrderHeaderRepository : Repository<OrderHeader>, IOrderHeaderRepository
    {
        private readonly ApplicationDbContext _Context;
        public OrderHeaderRepository(ApplicationDbContext Context):base(Context)
        {
            _Context = Context;
        }
        public void Update(OrderHeader orderHeader)
        {
            _Context.Update(orderHeader);
        }
    }
}
