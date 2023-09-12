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
    public class ShoppingCartRepository : Repository<ShoppingCart>, IShoppingCartRepository
    {
        private readonly ApplicationDbContext _Context;
        public ShoppingCartRepository(ApplicationDbContext context):base(context)
        {
            _Context = context;
        }
        public void Update(ShoppingCart shoppingCart)
        {
            _Context.Update(shoppingCart);
        }
    }
}
