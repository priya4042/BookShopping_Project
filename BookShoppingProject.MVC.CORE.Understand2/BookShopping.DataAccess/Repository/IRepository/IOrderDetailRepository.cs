using BookShopping.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookShopping.DataAccess.Repository.IRepository
{
   public interface IOrderDetailRepository:IRepository<OrderDetails>
    {
        void Update(OrderDetails orderDetails);
    }
}
