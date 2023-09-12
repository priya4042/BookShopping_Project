using BookShoppingProject.DataAccess.Repository.IRepository;
using BookShoppingProject.Models;
using BookShoppingProject_MVC_CORE_UnderStanding3.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookShoppingProject.DataAccess.Repository
{
    class ApplicationReposotory:Repository<ApplicatiionUser>, IApplicationReposotory
    {
        private readonly ApplicationDbContext _context;
        public ApplicationReposotory(ApplicationDbContext context):base(context)
        {
            _context = context;
        }
    }
}
