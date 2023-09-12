﻿using Bookshopping.DataAccess.Repository.IRepository;
using Bookshopping.Models;
using BookShopping_MVC_Core_Understanding.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bookshopping.DataAccess.Repository
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
                if (productInDb.ImageUrl != "")
                    productInDb.ImageUrl = product.ImageUrl;
                productInDb.Title = product.Title;
                productInDb.Discription = product.Discription;
            }
        }
    }
}
