using Dapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static Dapper.SqlMapper;

namespace BookShopingProject.DataAccess.Repository.IRepository
{
    public interface ISP_Call : IDisposable
    {
        T single<T>(string procedureName, DynamicParameters param = null);
        T ONeRecored<T>(string procedureName, DynamicParameters param = null);
        void Execute(string procedureName, DynamicParameters param = null);
        IEnumerable<T> List<T>(String prcedureName, DynamicParameters param = null);
        Tuple<IEnumerable<T1>, IEnumerable<T2>> List<T1, T2>(string procedureName, DynamicParameters param = null);

    }
}
