using BookShoping_Data_Access.Repository.IRepository;
using BookShoping_Models;
using BookShopingProject.Data_Access.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookShoping_Data_Access.Repository
{
    class CoverTypeRepository : Repository<CoverType>, ICoverTypeRepository
    {
        private readonly ApplicationDbContext _Context;
        public CoverTypeRepository(ApplicationDbContext context):base(context)
        {
            _Context = context;
        }


        public void update(CoverType CoverType)
        {
            _Context.Update(CoverType);
        }
    }
}
