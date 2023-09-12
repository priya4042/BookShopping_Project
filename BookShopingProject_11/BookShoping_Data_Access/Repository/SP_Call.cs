using BookShoping_Data_Access.Repository.IRepository;
using BookShopingProject.Data_Access.Data;
using Dapper;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookShoping_Data_Access.Repository
{
    public class SP_Call : ISP_Call
    {
        private readonly ApplicationDbContext _Context;

        private static string connectionstring ="";
        public SP_Call(ApplicationDbContext context)
        {
            _Context = context;
            connectionstring = _Context.Database.GetDbConnection().ConnectionString;
        }
        public void Dispose()
        {
            _Context.Dispose();
        }

        public void Execute(string procedureName, DynamicParameters param = null)
        {
            using (SqlConnection SqlCon = new SqlConnection(connectionstring))
            {
                SqlCon.Open();
                SqlCon.Execute(procedureName, param, commandType:CommandType.StoredProcedure);
            }
        }

        public IEnumerable<T> List<T>(string procedureName, DynamicParameters param = null)
        {
            using (SqlConnection SqlCon = new SqlConnection(connectionstring))
            {
                SqlCon.Open();
                return SqlCon.Query<T>(procedureName, param, commandType: CommandType.StoredProcedure);
            }
        }

        public Tuple<IEnumerable<T1>, IEnumerable<T2>> List<T1, T2>(string procedureName, DynamicParameters param = null)
        {
            using (SqlConnection SqlCon=new SqlConnection(connectionstring))
            {
                SqlCon.Open();
                var Result = SqlMapper.QueryMultiple(SqlCon, procedureName, param, commandType: CommandType.StoredProcedure);
                var item1 = Result.Read<T1>();
                var item2 = Result.Read<T2>();
                if (item1 != null && item2 != null)
                    return new Tuple<IEnumerable<T1>, IEnumerable<T2>>(item1, item2);
            }
            return new Tuple<IEnumerable<T1>, IEnumerable<T2>>(new List<T1>(), new List<T2>());
        }

        public T OneRecord<T>(string procedureName, DynamicParameters param = null)
        {
            using(SqlConnection SqlCon = new SqlConnection(connectionstring))
            {
                SqlCon.Open();
                var value = SqlCon.Query<T>(procedureName, param, commandType: CommandType.StoredProcedure);
                return value.FirstOrDefault();
            }
            
        }

        public T Single<T>(string procedureName, DynamicParameters param = null)
        {
            using (SqlConnection SqlCon = new SqlConnection(connectionstring))
            {
                SqlCon.Open();
                return SqlCon.ExecuteScalar<T>(procedureName, param, commandType: CommandType.StoredProcedure);
            }
        }
    }
}
