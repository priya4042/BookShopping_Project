using Bookshopping.DataAccess.Repository.IRepository;
using BookShopping_MVC_Core_Understanding.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bookshopping.DataAccess.Repository
{
    public class UnitofWork : IUnitofWork
    {
        private readonly ApplicationDbContext _context;
        public UnitofWork(ApplicationDbContext context)
        {
            _context = context;
            category = new CategoryRepository(_context);
            CoverType = new CoverTypeRepository(_context);
            Product = new ProductRepository(_context);
            Company = new CompanyRepository(_context);
            ApplicationUser = new ApplicationUserRepository(_context);
            shoppingCart = new ShoppingCartRepository(_context);
            orderHeader = new OrderHeaderRepository(_context);
            orderdetail = new OderDetailRepository(_context);
            
        }
        public ICategoryRepository category{ get; private set; }

        public ICoverTypeRepository CoverType { get; private set; }

        public IProductRepository Product { get; private set; }

        public ICompanyRepository Company { get; private set; }

        public IApplicationUserRepository ApplicationUser { get; private set; }

        

        public IOrderdetailRepository orderdetail { get; private set; }

        public IOrderHeaderRepository orderHeader { get; private set; }

        public IShoppingCartRepository shoppingCart { get; private set; }

        public void Save()
        {
            _context.SaveChanges();
        }
    }
}
