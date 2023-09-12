using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookShopingProject.DataAccess.Repository.IRepository
{
   public interface IUnitofWork
    {
        ICategoryRepostory Category { get; }
        ICoverTypeRepository CoverType { get; }
        ISP_Call SP_Call { get; }

        void Save();
    }
}
