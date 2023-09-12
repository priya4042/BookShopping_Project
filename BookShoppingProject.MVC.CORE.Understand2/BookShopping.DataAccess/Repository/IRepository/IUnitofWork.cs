using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookShopping.DataAccess.Repository.IRepository
{
   public interface IUnitofWork
    {
        ICategoryRepository category { get; }

        ICoverTypeRepository coverType { get; }

        IProductRepository Product { get; }

        ICompanyRepository Company { get; }
        IApplicationRepository Application { get; }
        IShoppingCartRepository shoppingCart { get; }
        IOrderDetailRepository orderdetail { get; }
        IOrderHeaderRepository orderHeader { get; }

        void save();
    }
}
