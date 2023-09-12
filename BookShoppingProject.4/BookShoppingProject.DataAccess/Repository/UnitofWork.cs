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
        private readonly ApplicationDbContext _Context;
        public UnitofWork(ApplicationDbContext context)
        {
            _Context = context;
            category = new CategoryRepository(_Context);
            coverType = new CoverTypeRepository(_Context);
        }
        public ICategoryRepository category { get; private set; }

        public ICoverTypeRepository coverType { get; private set; }

        public void Save()
        {
            _Context.SaveChanges();
        }
    }
}
