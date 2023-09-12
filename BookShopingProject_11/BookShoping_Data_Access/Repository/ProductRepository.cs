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
    public class ProductRepository : Repository<Product>, IProductRepository
    {
        private readonly ApplicationDbContext _context;
        public ProductRepository(ApplicationDbContext context):base(context)
        {
            _context = context;
        }
        public void Update(Product product)
        {
            var productInDb = _context.Products.FirstOrDefault(p => p.Id == product.Id);
            if(productInDb != null)
            {
                if (productInDb.ImageUrl != "")
                    productInDb.ImageUrl = product.ImageUrl;
                productInDb.Title = product.Title;
                productInDb.Discription = product.Discription;
                productInDb.ISBN = product.ISBN;
                productInDb.Author = product.Author;
                productInDb.ListPrice = product.ListPrice;
                productInDb.Price50 = product.Price50;
                productInDb.Price100 = product.Price100;
                productInDb.Price = product.Price;
                productInDb.CategoryId = product.CategoryId;
                productInDb.CoverTypeId = product.CoverTypeId;
            }
        }
    }
}
