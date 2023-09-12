﻿using BookShoping_Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookShoping_Data_Access.Repository.IRepository
{
   public interface ICompanyRepository:IRepository<Company>
    {
        void Update(Company company);
    }
}