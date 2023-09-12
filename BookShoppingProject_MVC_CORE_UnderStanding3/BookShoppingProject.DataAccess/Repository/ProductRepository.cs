using BookShoppingProject.DataAccess.Repository.IRepository;
using BookShoppingProject.Models;
using BookShoppingProject_MVC_CORE_UnderStanding3.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookShoppingProject.DataAccess.Repository
{
    public class ProductRepository : Repository<Product>, IProductRepository
    {
        private readonly ApplicationDbContext _context;
        public ProductRepository(ApplicationDbContext context):base(context)
        {
            _context = context;
        }
        public void update(Product product)
        {
            var productIndb = _context.Products.FirstOrDefault(p => p.Id == product.Id);
            if(productIndb != null)
            {
                if (productIndb.ImageUrl != " ")
                    productIndb.ImageUrl = product.ImageUrl;
                productIndb.Title = product.Title;
                productIndb.Description = product.Description;
            }
        }
    }
}
