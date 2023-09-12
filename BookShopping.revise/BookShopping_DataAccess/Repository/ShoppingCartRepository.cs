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
    public class ShoppingCartRepository : Repository<ShoppingCarts>, IShoppingCartRepository
    {
        private readonly ApplicationDbContext _Context;
        public ShoppingCartRepository(ApplicationDbContext context):base(context)
        {
            _Context = context;
        }
        public void Update(ShoppingCarts shoppingCart)
        {
            _Context.Update(shoppingCart);
        }
    }
}
