﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookShoppingProject.DataAccess.Repository.IRepository
{
   public interface IUnitofWork
    {
        ICategoryRepository Category { get; }

        ICoverTypeRepository CoverType { get; }

        IProductRepository Product { get; }
        ICompanyRepository Company { get; }
        IApplicationReposotory Application { get; }
        IShoppingCartRepository shoppingCart { get; }
        IOrderDetailsRepository orderdetail { get; }
        IOrderHeaderRepository orderHeader { get; }

        void Save();
    }
}
