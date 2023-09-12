using BookShoppingProject.DataAccess.Repository.IRepository;
using BookShoppingProject_MVC_CORE_UnderStanding3.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookShoppingProject.DataAccess.Repository
{
    public class UnitofWork : IUnitofWork
    {
        private readonly ApplicationDbContext _context;
        public UnitofWork(ApplicationDbContext context)
        {
            _context = context;
            Category = new CategoryRepository(_context);
            CoverType = new CoverTypeRepository(_context);
            Product = new ProductRepository(_context);
            Company = new CompanyRepository(_context);
            Application = new ApplicationReposotory(_context);
            shoppingCart = new ShoppingCartRepository(_context);
            orderHeader = new OrderHeaderRepository(_context);
            orderdetail = new OrderDetailRepository(_context);
        }
        public ICategoryRepository Category { get; private set; }

        public ICoverTypeRepository CoverType { get; private set; }

        public IProductRepository Product { get; private set; }

        public ICompanyRepository Company { get; private set; }

        public IApplicationReposotory Application { get; private set; }

        public IShoppingCartRepository shoppingCart { get; private set; }

        public IOrderDetailsRepository orderdetail { get; private set; }

        public IOrderHeaderRepository orderHeader { get; private set; }

        public void Save()
        {
            _context.SaveChanges();
        }
    }
}
