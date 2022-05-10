using Dapper;
using System.Data;
using System.Data.Common;
using System.Data.SqlClient;
using static Dapper.SqlMapper;

namespace Medicorp.Data.InterFace
{
    public interface IDapperHelper
    {
        void ChangeConnection(string ConnectionName);
        DbConnection GetConnection();
        Task<T> GetAsync<T>(string sp, DynamicParameters parms, CommandType commandType = CommandType.StoredProcedure);
        Task<List<T>> GetAllAsync<T>(string sp, DynamicParameters parms, CommandType commandType = CommandType.StoredProcedure, int commandTimeout = 25);
        Task<int> ExecuteAsync(string sp, DynamicParameters parms, CommandType commandType = CommandType.StoredProcedure);
        Task<Tuple<IEnumerable<T1>, IEnumerable<T2>>> GetMultipleAsync<T1, T2>(string sql, DynamicParameters parameters,
                                          Func<GridReader, IEnumerable<T1>> func1,
                                          Func<GridReader, IEnumerable<T2>> func2);
        Task<Tuple<IEnumerable<T1>, IEnumerable<T2>, IEnumerable<T3>>> GetMultipleAsync<T1, T2, T3>(string sql, DynamicParameters parameters,
                                       Func<GridReader, IEnumerable<T1>> func1,
                                       Func<GridReader, IEnumerable<T2>> func2,
                                       Func<GridReader, IEnumerable<T3>> func3);
        DataTable GetDataSet(string sp, SqlParameter[] parms, CommandType command = CommandType.StoredProcedure);
    }
}
