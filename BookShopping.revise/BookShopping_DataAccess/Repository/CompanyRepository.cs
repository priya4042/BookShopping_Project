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
    public class CompanyRepository : Repository<Company>, ICompanyRepository
    {
        private readonly ApplicationDbContext _Context;
        public CompanyRepository(ApplicationDbContext context):base(context)
        {
            _Context = context;
        }
        public void Update(Company company)
        {
            _Context.Update(company);
        }
    }
}
