using BookShopping.revise.Data;
using BookShopping_DataAccess.Repository.IRepository;
using BookShopping_Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookShopping_DataAccess.Repository
{
    public class CoverTypeRepository : Repository<CoverType>, ICoverTypeRepository
    {
        private readonly ApplicationDbContext _context;
        public CoverTypeRepository(ApplicationDbContext context):base(context)
        {
            _context = context;
        }
        public void update(CoverType coverType)
        {
            _context.Update(coverType);
        }
    }
}
