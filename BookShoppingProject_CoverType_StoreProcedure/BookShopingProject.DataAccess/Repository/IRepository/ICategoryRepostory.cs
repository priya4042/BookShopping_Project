using BookShopingProject.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookShopingProject.DataAccess.Repository.IRepository
{
   public interface ICategoryRepostory:IRepository<Category>
    {
        void Update(Category category);
    }
}
