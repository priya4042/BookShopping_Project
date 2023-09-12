using BookShoping_Data_Access.Repository.IRepository;
using BookShopingProject.Data_Access.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookShoping_Data_Access.Repository 
{
    public class UnitofWork:IUnitofWork
    {
        private readonly ApplicationDbContext _context;
        public UnitofWork(ApplicationDbContext context)
        {
            _context = context;
            Category = new CategoryRepository(_context);
            coverType = new CoverTypeRepository(_context);
            SP_Call = new SP_Call(_context);
            Product = new ProductRepository(_context);
            company = new CompanyRepository(_context);
            ApplicationUser = new ApplicationUserRepository(_context);
            ShoppingCart = new ShoppingCartRepository(_context);
            OrderHeader = new OrderHeaderRepository(_context);
            OrderDetails = new OrderDetailsRepository(_context);
        }

        public IcategoryRepository Category { get; private set; }

        public ICoverTypeRepository coverType { get; private set; }

        public ISP_Call SP_Call { get; private set; }

        public ProductRepository Product { get; private set; }

        public CompanyRepository company { get; private set; }

        public ApplicationUserRepository ApplicationUser { get; private set; }

        public ShoppingCartRepository ShoppingCart { get; private set; }

        public OrderHeaderRepository OrderHeader { get; private set; }

        public OrderDetailsRepository OrderDetails { get; private set; }

        public void save()
        {
            _context.SaveChanges();

        }
    }
}
