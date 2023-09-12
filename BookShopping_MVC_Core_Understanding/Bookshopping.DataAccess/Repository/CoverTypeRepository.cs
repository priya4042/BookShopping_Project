using Bookshopping.DataAccess.Repository.IRepository;
using Bookshopping.Models;
using BookShopping_MVC_Core_Understanding.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bookshopping.DataAccess.Repository
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
