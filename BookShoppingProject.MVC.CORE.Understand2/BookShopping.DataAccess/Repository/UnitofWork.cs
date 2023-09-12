using BookShopping.DataAccess.Repository.IRepository;
using BookShoppingProject.MVC.CORE.Understand2.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookShopping.DataAccess.Repository
{
    public class UnitofWork : IUnitofWork
    {
        private readonly ApplicationDbContext _context;
        public UnitofWork(ApplicationDbContext context)
        {
            _context = context;
            category = new CategoryRepository(_context);
            coverType = new CoverTypeRepository(_context);
            Product = new ProductRepository(_context);
            Company = new CompanyRepository(_context);
            Application = new ApplicationRepository(_context);
            shoppingCart = new ShoppingCartRepository(_context);
            orderHeader = new OrdeHeaderRepository(_context);
            orderdetail = new OrderDetailRepository(_context);


        }
        public ICategoryRepository category { get; private set; }

        public ICoverTypeRepository coverType { get; private set; }

        public IProductRepository Product { get; private set; }

        public ICompanyRepository Company { get; private set; }

        public IApplicationRepository Application { get; private set; }

        public IShoppingCartRepository shoppingCart { get; private set; }

        public IOrderDetailRepository orderdetail { get; private set; }

        public IOrderHeaderRepository orderHeader { get; private set; }

        public void save()
        {
            _context.SaveChanges();
        }
    }
}
