using BookShopping_Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookShopping_DataAccess.Repository.IRepository
{
   public interface ICoverTypeRepository:IRepository<CoverType>
    {
        void update(CoverType coverType);
    }
}
