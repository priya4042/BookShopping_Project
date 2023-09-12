using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookShoping_Data_Access.Repository.IRepository
{
  public  interface IUnitofWork
    {
        IcategoryRepository  Category { get; }
        ICoverTypeRepository coverType { get; }
        ISP_Call SP_Call { get; }
        ProductRepository Product { get; }
        CompanyRepository company { get; }
        ApplicationUserRepository ApplicationUser { get; }
        ShoppingCartRepository ShoppingCart { get; }
        OrderHeaderRepository OrderHeader { get; }
        OrderDetailsRepository OrderDetails { get; }

        void save();
    }
}
