using Dapper;
using Medicorp.Core;
using Medicorp.Core.CustomException;
using Medicorp.Core.Entity;
using Medicorp.Data.InterFace;
using Medicorp.IServices;
using System.Data;
using System.Transactions;

namespace Medicorp.Services
{
    public class OrganizationMasterService : IOrganizationMaster

    {
        private readonly IDapperHelper _dapperHelper;
        public OrganizationMasterService(IDapperHelper dapperHelper)
        {
            _dapperHelper = dapperHelper;
        }
        async Task<ApiResponse<List<OrganizationMaster>>> IOrganizationMaster.GetOrganizationAsync(OrganizationMasterFilter filter)
        {
            return await GetOrganizationAsync<OrganizationMaster>(filter);
        }
        public async Task<ApiResponse<List<T>>> GetOrganizationAsync<T>(OrganizationMasterFilter filter) where T : class
        {
            ApiResponse<List<T>> response = new ApiResponse<List<T>>() { Success = true };
            try
            {
                DynamicParameters dbPara = new DynamicParameters();
                dbPara.Add("@OrganizationId", filter.OrganizationId, DbType.Int32);
                dbPara.Add("@OrganizationName", filter.OrganizationName, DbType.String);
                dbPara.Add("@IsActive", filter.IsActive, DbType.Boolean);
                response.Result = await _dapperHelper.GetAllAsync<T>(sp: SqlObjectName.OrganizationMasterSelect,
                                              parms: dbPara,
                                              commandType: CommandType.StoredProcedure);
            }
            catch (Exception ex)
            {
                response.ConstructErrorResponse("OrganizationMasterService GetOrganizationAsync", ex.Message);
            }
            return response;
        }
    }
}
