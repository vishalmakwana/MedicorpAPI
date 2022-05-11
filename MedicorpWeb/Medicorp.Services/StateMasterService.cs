using Dapper;
using Medicorp.Core;
using Medicorp.Core.CustomException;
using Medicorp.Core.Entity.Master;
using Medicorp.Data.InterFace;
using Medicorp.IServices;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Transactions;

namespace Medicorp.Services
{
    public class StateMasterService : IStateMasterService
    {
        private readonly IDapperHelper _dapperHelper;
        public StateMasterService(IDapperHelper dapperHelper)
        {
            _dapperHelper = dapperHelper;
        }
        async Task<ApiResponse<int>> IStateMasterService.CreateAsync(StateMaster stateMaster)
        {
            ApiResponse<int> response = new ApiResponse<int>() { Success = true };
            using (TransactionScope transactionScope = new TransactionScope(TransactionScopeAsyncFlowOption.Enabled))
            {
                try
                {
                    if (string.IsNullOrEmpty(stateMaster.StateName))
                        throw new OperationExecutionException("State Name is not valid");

                    DynamicParameters dbPara = new DynamicParameters();
                    dbPara.Add("@StateName", stateMaster.StateName, DbType.String);
                    dbPara.Add("@IsActive", stateMaster.IsActive, DbType.Boolean);
                    response.Result = await _dapperHelper.GetAsync<int>(sp: SqlObjectName.StateMasterInsert,
                                                  parms: dbPara,
                                                  commandType: CommandType.StoredProcedure);

                    transactionScope.Complete();
                }
                catch (Exception ex)
                {
                    response.ConstructErrorResponse("StateMasterService CreateAsync", ex.Message);
                }
            }
            return response;
        }
        async Task<ApiResponse<List<StateMaster>>> IStateMasterService.GetStateAsync(StateMaster stateMaster)
        {
            return await GetStateAsync<StateMaster>(stateMaster);
        }
        public async Task<ApiResponse<List<T>>> GetStateAsync<T>(StateMaster stateMaster) where T : class
        {
            ApiResponse<List<T>> response = new ApiResponse<List<T>>() { Success = true };
            try
            {
                DynamicParameters dbPara = new DynamicParameters();
                dbPara.Add("@StateName", stateMaster.StateName, DbType.String);
                dbPara.Add("@IsActive", stateMaster.IsActive, DbType.Boolean);
                response.Result = await _dapperHelper.GetAllAsync<T>(sp: SqlObjectName.StateMasterSelect,
                                              parms: dbPara,
                                              commandType: CommandType.StoredProcedure);
            }
            catch (Exception ex)
            {
                response.ConstructErrorResponse("StateMasterService GetProductAsync", ex.Message);
            }
            return response;
        }
    }
}
