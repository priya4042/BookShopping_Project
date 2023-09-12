using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookShoppingProject.DataAccess.Repository.IRepository
{
   public interface IUnitWork
    {
        ICategoryRepository category { get; }

        ICoverTypeRepository coverType { get; }

        ISP_Call SP_Call { get; }

        void save();
    }
}
