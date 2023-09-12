using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bookshopping.DataAccess.Repository.IRepository
{
   public interface IUnitofWork
    {
        ICategoryRepository category { get; }
        ICoverTypeRepository CoverType { get; }
        IProductRepository Product { get; }
        ICompanyRepository Company { get; }
        IApplicationUserRepository ApplicationUser { get; }
        IShoppingCartRepository shoppingCart { get; }
        IOrderdetailRepository orderdetail { get; }
        IOrderHeaderRepository orderHeader { get; }
     
         void Save();
    }
}
