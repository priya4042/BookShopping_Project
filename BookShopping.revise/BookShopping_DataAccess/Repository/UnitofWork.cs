using BookShopping.revise.Data;
using BookShopping_DataAccess.Repository.IRepository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookShopping_DataAccess.Repository
{
    public class UnitofWork : IunitofWork
    {
        private readonly ApplicationDbContext _context;
        public UnitofWork(ApplicationDbContext context)
        {
            _context = context;
            Category = new CategoryRepository(context);
            CoverType = new CoverTypeRepository(context);
            Product = new ProductRepository(context);
            Company = new CompanyRepository(context);
            ApplicationUser = new ApplicationUserRepositore(context);
            ShoppingCart = new ShoppingCartRepository(context);
            OrderDetail = new OrderDetailrePository(context);
            OrderHeader = new OrderHearderRepository(context);
        }

        public ICategoryRepository Category { get; private set; }

        public ICoverTypeRepository CoverType { get; private set; }

        public IProductRepository Product { get; private set; }

        public ICompanyRepository Company { get; private set; }

        public IApplicationUserRepositore ApplicationUser { get; private set; }

        public IShoppingCartRepository ShoppingCart { get; private set; }

        public IOrderHeaderRepository OrderHeader { get; private set; }

        public IOrderDetailRepository OrderDetail { get; private set; }

        public void Save()
        {
            _context.SaveChanges();
        }
    }
}
