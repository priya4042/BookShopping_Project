using BookShopingProject.DataAccess.Repository.IRepository;
using BookShoppingProject_CoverType_StoreProcedure.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookShopingProject.DataAccess.Repository
{
    public class UnitofWork : IUnitofWork
    {
        private readonly ApplicationDbContext _context;
        public UnitofWork(ApplicationDbContext context)
        {
            _context = context;
            Category = new CategoryRepostory(_context);
            CoverType = new CoverTypeRepository(_context);
            SP_Call = new SP_Call(_context);
        }
        public ICategoryRepostory Category { get; private set; }

        public ICoverTypeRepository CoverType { get; private set; }

        public ISP_Call SP_Call { get; private set; }

        public void Save()
        {
            _context.SaveChanges();
        }
    }
}
