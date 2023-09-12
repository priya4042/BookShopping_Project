using BookShopping.revise.Data;
using BookShopping_DataAccess.Repository.IRepository;
using BookShopping_Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookShopping_DataAccess.Repository
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
            var productInDb = _Context.Products.FirstOrDefault(p => p.Id == product.Id);
            if(productInDb != null)
            {
                if (productInDb.ImageURl != "")
                    productInDb.ImageURl = product.ImageURl;
                productInDb.Title = product.Title;
                productInDb.Discription = product.Discription;
            }
        }
    }
}
