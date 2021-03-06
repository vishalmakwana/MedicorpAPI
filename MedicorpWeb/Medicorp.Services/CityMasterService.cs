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
    public class CityMasterService : ICityMasterService
    {
        private readonly IDapperHelper _dapperHelper;
        public CityMasterService(IDapperHelper dapperHelper)
        {
            _dapperHelper = dapperHelper;
        }
        async Task<ApiResponse<int>> ICityMasterService.CreateAsync(CityMaster cityMaster)
        {
            ApiResponse<int> response = new ApiResponse<int>() { Success = true };
            Validation validation = new Validation();
            validation.keys = new List<string>();

            using (TransactionScope transactionScope = new TransactionScope(TransactionScopeAsyncFlowOption.Enabled))
            {
                try
                {
                    if (string.IsNullOrEmpty(cityMaster.CityName))
                        validation.source = "City Name";
                        validation.keys.Add("City Name can not be allow null or empty.");
                  
                    bool validationResponse = await ValidateNameAsync(cityMaster.CityName);
                    if (!validationResponse)
                        validation.source = "City Name";
                        validation.keys.Add("City Name is already exist.");

                    if (cityMaster.StateId<=0)
                        validation.source = "StateId";
                        validation.keys.Add("StateId can not be allow 0."); 

                    DynamicParameters dbPara = new DynamicParameters();
                    dbPara.Add("@CityName", cityMaster.CityName, DbType.String);
                    dbPara.Add("@StateId", cityMaster.StateId, DbType.Int32);
                    dbPara.Add("@IsActive", cityMaster.IsActive, DbType.Boolean);
                    response.Result = await _dapperHelper.GetAsync<int>(sp: SqlObjectName.CityMasterInsert,
                                                  parms: dbPara,
                                                  commandType: CommandType.StoredProcedure);

                    transactionScope.Complete();
                }
                catch (Exception ex)
                {
                    response.ConstructErrorResponse("CityMasterService CreateAsync", ex.Message);
                }
            }
            response.validation = validation;
            return response;
        }
        async Task<ApiResponse<List<CityMaster>>> ICityMasterService.GetCityAsync(CityMaster cityMaster)
        {
            return await GetCityAsync<CityMaster>(cityMaster);
        }
        public async Task<ApiResponse<List<T>>> GetCityAsync<T>(CityMaster cityMaster) where T : class
        {
            ApiResponse<List<T>> response = new ApiResponse<List<T>>() { Success = true };
            try
            {
                DynamicParameters dbPara = new DynamicParameters();
                dbPara.Add("@CityName", cityMaster.CityName, DbType.String);
                dbPara.Add("@StateId", cityMaster.StateId, DbType.Int32);
                dbPara.Add("@IsActive", cityMaster.IsActive, DbType.Boolean);
                response.Result = await _dapperHelper.GetAllAsync<T>(sp: SqlObjectName.CityMasterSelect,
                                              parms: dbPara,
                                              commandType: CommandType.StoredProcedure);
            }
            catch (Exception ex)
            {
                response.ConstructErrorResponse("CityMasterService GetProductAsync", ex.Message);
            }
            return response;
        }
        public async Task<bool> ValidateNameAsync(string CityName)
        {
            DynamicParameters dbPara = new DynamicParameters();
            dbPara.Add("@CityName", CityName, DbType.String);
            bool response = await _dapperHelper.GetAsync<bool>(sp: SqlObjectName.CityMasterValidate,
                                            parms: dbPara,
                                            commandType: CommandType.StoredProcedure);
            return response;
        }
    }
}
