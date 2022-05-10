using Dapper;
using Medicorp.Data.InterFace;
using Microsoft.Extensions.Configuration;
using System.Data;
using System.Data.Common;
using System.Data.SqlClient;
using static Dapper.SqlMapper;

namespace Medicorp.Data
{
    public class DapperHelper : IDapperHelper
    {
        private readonly IConfiguration _config;
        private string ConnectionName = "SqlConnection";
        public DapperHelper(IConfiguration config)
        {
            _config = config;
        }
        public void ChangeConnection(string ConnectionName) => this.ConnectionName = ConnectionName;

        public DbConnection GetConnection()
        {
            return new SqlConnection(_config.GetConnectionString(ConnectionName));
        }

        public async Task<T> GetAsync<T>(string sp, DynamicParameters parms, CommandType commandType = CommandType.StoredProcedure)
        {
            using (IDbConnection db = new SqlConnection(_config.GetConnectionString(ConnectionName)))
            {
                IEnumerable<T> output = await db.QueryAsync<T>(sp, parms, commandType: commandType);
                return output.FirstOrDefault();
            }
        }

        public async Task<List<T>> GetAllAsync<T>(string sp, DynamicParameters parms, CommandType commandType = CommandType.StoredProcedure,
            int commandTimeout = 25)
        {
            using (IDbConnection db = new SqlConnection(_config.GetConnectionString(ConnectionName)))
            {
                IEnumerable<T> output = await db.QueryAsync<T>(sp, parms, commandType: commandType, commandTimeout: commandTimeout);
                return output.ToList();
            }
        }

        #region For MultiQuery
        public async Task<Tuple<IEnumerable<T1>, IEnumerable<T2>>> GetMultipleAsync<T1, T2>(string sql, DynamicParameters parameters,
                                        Func<GridReader, IEnumerable<T1>> func1,
                                        Func<GridReader, IEnumerable<T2>> func2)
        {
            List<object> objs = await getMultipleAsync(sql, parameters, func1, func2);
            return Tuple.Create(objs[0] as IEnumerable<T1>, objs[1] as IEnumerable<T2>);
        }

        public async Task<Tuple<IEnumerable<T1>, IEnumerable<T2>, IEnumerable<T3>>> GetMultipleAsync<T1, T2, T3>(string sql, DynamicParameters parameters,
                                               Func<GridReader, IEnumerable<T1>> func1,
                                               Func<GridReader, IEnumerable<T2>> func2,
                                               Func<GridReader, IEnumerable<T3>> func3)
        {
            List<object> objs = await getMultipleAsync(sql, parameters, func1, func2, func3);
            return Tuple.Create(objs[0] as IEnumerable<T1>, objs[1] as IEnumerable<T2>, objs[2] as IEnumerable<T3>);
        }

        public async Task<List<object>> getMultipleAsync(string sql, DynamicParameters parameters, params Func<GridReader, object>[] readerFuncs)
        {
            List<object> returnResults = new List<object>();
            using (IDbConnection db = new SqlConnection(_config.GetConnectionString(ConnectionName)))
            {
                GridReader gridReader = await db.QueryMultipleAsync(sql, parameters, commandType: CommandType.StoredProcedure);

                foreach (Func<GridReader, object> readerFunc in readerFuncs)
                {
                    object obj = readerFunc(gridReader);
                    returnResults.Add(obj);
                }
            }
            return returnResults;
        }

        public DataTable GetDataSet(string sp, SqlParameter[] parms, CommandType command = CommandType.StoredProcedure)
        {
            using (SqlConnection connection = new SqlConnection(_config.GetConnectionString(ConnectionName)))
            {
                using (SqlCommand cmd = new SqlCommand(sp, connection)
                {
                    CommandType = command
                })
                {
                    cmd.Parameters.AddRange(parms);
                    using (SqlDataAdapter da = new SqlDataAdapter(cmd))
                    {
                        DataTable returnTable = new DataTable();
                        da.Fill(returnTable);
                        return returnTable;
                    }
                }
            }
        }

        public async Task<int> ExecuteAsync(string sp, DynamicParameters parms, CommandType commandType = CommandType.StoredProcedure)
        {
            using (IDbConnection db = new SqlConnection(_config.GetConnectionString(ConnectionName)))
            {
                int output = await db.ExecuteAsync(sp, parms, commandType: commandType);
                return output;
            }
        }
        #endregion
    }
}
