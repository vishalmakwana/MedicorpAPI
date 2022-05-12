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

        public async Task<ApiResponse<int>> CreateAsync(OrganizationMaster organizationMaster)
        {
            ApiResponse<int> response = new ApiResponse<int>() { Success = true };
            using (TransactionScope transactionScope = new TransactionScope(TransactionScopeAsyncFlowOption.Enabled))
            {
                try
                {
                    if (string.IsNullOrEmpty(organizationMaster.OrganizationName))
                        throw new OperationExecutionException("Organization Name is not valid");
                    bool validationResponse = await ValidateNameAsync(organizationMaster.OrganizationName);
                    if (!validationResponse)
                        throw new OperationExecutionException("Organization Name is already exists");

                    DynamicParameters dbPara = new DynamicParameters();
                    dbPara.Add("@OrganizationName", organizationMaster.OrganizationName, DbType.String);
                    dbPara.Add("@OrganizationPrefix", organizationMaster.OrganizationPrefix, DbType.String);
                    dbPara.Add("@IsActive", organizationMaster.IsActive, DbType.Boolean);
                    dbPara.Add("@InsertBy", organizationMaster.InsertBy, DbType.String);
                    dbPara.Add("@InsertDate", organizationMaster.InsertedDate, DbType.DateTime);
                    response.Result = await _dapperHelper.GetAsync<int>(sp: SqlObjectName.OrganizationMasterInsert,
                                                  parms: dbPara,
                                                  commandType: CommandType.StoredProcedure);

                    transactionScope.Complete();
                }
                catch (Exception ex)
                {
                    response.ConstructErrorResponse(" OrganizationMasterService CreateAsync", ex.Message);
                }
            }
            return response;
        }

        public async Task<ApiResponse<int>> DeleteAsync(int OrganizationId)
        {
            ApiResponse<int> response = new ApiResponse<int>() { Success = true };
            using (TransactionScope transactionScope = new TransactionScope(TransactionScopeAsyncFlowOption.Enabled))
            {
                try
                {
                    DynamicParameters dbPara = new DynamicParameters();
                    dbPara.Add("@OrganizationId", OrganizationId, DbType.Int32);
                    response.Result = await _dapperHelper.ExecuteAsync(sp: SqlObjectName.OrganizationMasterDelete,
                                                  parms: dbPara,
                                                  commandType: CommandType.StoredProcedure);

                    transactionScope.Complete();
                }
                catch (Exception ex)
                {
                    response.ConstructErrorResponse("OrganizationMasterService DeleteAsync", ex.Message);
                }

                return response;
            }
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

        public async Task<ApiResponse<int>> UpdateAsync(OrganizationMaster organizationMaster)
        {
            ApiResponse<int> response = new ApiResponse<int>() { Success = true };
            using (TransactionScope transactionScope = new TransactionScope(TransactionScopeAsyncFlowOption.Enabled))
            {
                try
                {
                    if (organizationMaster.OrganizationId == 0)
                        throw new OperationExecutionException("Organization Id is not valid");
                    if (string.IsNullOrEmpty(organizationMaster.OrganizationName))
                        throw new OperationExecutionException("Organization name is not valid");
                    bool validationResponse = await ValidateAsync(organizationMaster.OrganizationId, organizationMaster.OrganizationName);
                    if (!validationResponse)
                        throw new OperationExecutionException("Organization name is already exists");

                    DynamicParameters dbPara = new DynamicParameters();
                    dbPara.Add("@OrganizationId", organizationMaster.OrganizationId, DbType.Int32);
                    dbPara.Add("@OrganizationName", organizationMaster.OrganizationName, DbType.String);
                    dbPara.Add("@OrganizationPrefix", organizationMaster.OrganizationPrefix, DbType.String);
                    dbPara.Add("@IsActive", organizationMaster.IsActive, DbType.Boolean);
                    dbPara.Add("@UpdateBy", organizationMaster.UpdateBy, DbType.String);
                    dbPara.Add("@UpdatedDate", organizationMaster.UpdateDate, DbType.DateTime);
                    response.Result = await _dapperHelper.GetAsync<int>(sp: SqlObjectName.OrganizationMasterUpdate,
                                                  parms: dbPara,
                                                  commandType: CommandType.StoredProcedure);

                    transactionScope.Complete();
                }
                catch (Exception ex)
                {
                    response.ConstructErrorResponse("OrganizationMasterService UpdateAsync", ex.Message);
                }
            
            }
            return response;
        }

        public async Task<bool> ValidateAsync(int OrganizationId, string OrganizationName)
        {
            DynamicParameters dbPara = new DynamicParameters();
            dbPara.Add("@OrganizationId", OrganizationId, DbType.Int32);
            dbPara.Add("@OrganizationName", OrganizationName, DbType.String);
            bool response = await _dapperHelper.GetAsync<bool>(sp: SqlObjectName.OrganizationMasterValidateOrganizationName,
                                            parms: dbPara,
                                            commandType: CommandType.StoredProcedure);
            return response;
        }
        public async Task<bool> ValidateNameAsync(string OrganizationName)
        {
            DynamicParameters dbPara = new DynamicParameters();
            dbPara.Add("@OrganizationName", OrganizationName, DbType.String);
            bool response = await _dapperHelper.GetAsync<bool>(sp: SqlObjectName.OrganizationMasterExistOrganizationName,
                                            parms: dbPara,
                                            commandType: CommandType.StoredProcedure);
            return response;
        }
    }
}
