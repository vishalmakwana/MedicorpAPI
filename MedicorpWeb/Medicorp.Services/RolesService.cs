using Dapper;
using Medicorp.Core;
using Medicorp.Core.Entity.Master;
using Medicorp.Data.InterFace;
using Medicorp.IServices;
using System.Data;


namespace Medicorp.Services
{
    public class RolesService : IRolesServicecs
    {
        private readonly IDapperHelper _dapperHelper;
        public RolesService(IDapperHelper dapperHelper)
        {
            _dapperHelper = dapperHelper;
        }
        async Task<ApiResponse<List<AspNetRoles>>> IRolesServicecs.GetRolesAsync(AspNetRolesFilter filter)
        {
            return await GetRolesAsync<AspNetRoles>(filter);
        }

       public async Task<ApiResponse<List<T>>> GetRolesAsync<T>(AspNetRolesFilter filter) where T:class
        {

            ApiResponse<List<T>> response = new ApiResponse<List<T>>() { Success = true };
            try
            {
                DynamicParameters dbPara = new DynamicParameters();
                dbPara.Add("@Name", filter.Name, DbType.String);
                dbPara.Add("@NormalizedName", filter.NormalizedName, DbType.String);
                dbPara.Add("@ConcurrencyStamp", filter.ConcurrencyStamp, DbType.String);
             
                response.Result = await _dapperHelper.GetAllAsync<T>(sp: SqlObjectName.ProductMasterSelect,
                                              parms: dbPara,
                                              commandType: CommandType.StoredProcedure);
            }
            catch (Exception ex)
            {
                response.ConstructErrorResponse("RolesService GetRolesAsync", ex.Message);
            }
            return response;
        }

       
    }
}
