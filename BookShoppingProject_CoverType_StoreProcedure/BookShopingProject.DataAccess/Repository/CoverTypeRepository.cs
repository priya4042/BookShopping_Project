using BookShopingProject.DataAccess.Repository.IRepository;
using BookShopingProject.Models;
using BookShoppingProject_CoverType_StoreProcedure.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookShopingProject.DataAccess.Repository
{
    public class CoverTypeRepository : Repository<CoverType>, ICoverTypeRepository
    {
        private readonly ApplicationDbContext _Context;
        public CoverTypeRepository(ApplicationDbContext context):base(context)
        {
            _Context = context;
        }
        public void Update(CoverType coverType)
        {
            _Context.Update(coverType);
        }
    }
}
