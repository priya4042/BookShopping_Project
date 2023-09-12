using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookShoppingProject.DataAccess.Repository.IRepository
{
   public interface IUnitofWork
    {
        ICategoryRepository category { get; }
        ICoverTypeRepository coverType { get; }

        void Save();
    }
}
