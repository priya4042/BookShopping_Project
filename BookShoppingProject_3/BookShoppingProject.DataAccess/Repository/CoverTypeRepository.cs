using BookShoppingProject.DataAccess.Data;
using BookShoppingProject.DataAccess.Repository.IRepository;
using BookShoppingProject.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookShoppingProject.DataAccess.Repository
{
    public class CoverTypeRepository : Repository<CoverType>, ICoverTypeRepository
    {
        private readonly ApplicationDbContext _Context;
        public CoverTypeRepository(ApplicationDbContext context):base(context)
        {
            _Context = context;
        }
        public void Update(CoverType coverTypes)
        {
            _Context.Update(coverTypes);
        }
    }
}
