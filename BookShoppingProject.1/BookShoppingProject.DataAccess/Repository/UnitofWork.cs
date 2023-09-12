using BookShoppingProject.DataAccess.Data;
using BookShoppingProject.DataAccess.Repository.IRepository;
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
            coverType = new CoverTypeRepository(_context);
        }
    public ICategoryRepository Category { get; private set; }

        public ICoverTypeRepository coverType { get; private set; }

        public void Save()
        {
            _context.SaveChanges();
        }
    }
}
