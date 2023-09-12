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
    public class ProductRepository : Repository<Product>, IProductRepository
    {
        private readonly ApplicationDbContext _Context;
        public ProductRepository(ApplicationDbContext context):base(context)
        {
            _Context = context;
        }
        public void Update(Product product)
        {
            var productIndb = _Context.Products.FirstOrDefault(p => p.Id == product.Id);
            if(productIndb != null)
            {
                if (productIndb.ImageUrl != "")
                    productIndb.ImageUrl = product.ImageUrl;
                productIndb.Title = product.Title;
                productIndb.Description = product.Description;
            }
        }
    }
}
