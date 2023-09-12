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
    public class CompanyRepository : Repository<Company>, ICompanyRepository
    {
        private readonly ApplicationDbContext _context;
        public CompanyRepository(ApplicationDbContext context):base(context)
        {
            _context = context;
        }
        public void Update(Company company)
        {
            _context.Companies.Update(company);
        }
    }
}
