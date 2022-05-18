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
    public class SpecialityMasterService : ISpecialityMasterService
    {
        private readonly IDapperHelper _dapperHelper;
        public SpecialityMasterService(IDapperHelper dapperHelper)
        {
            _dapperHelper = dapperHelper;
        }
        public async Task<ApiResponse<List<SpecilityMaster>>> GetSpecialityAsync(SpecilityMasterFilter filter)
        {
            return await GetSpecialityAsync<SpecilityMaster>(filter);
        }
        public async Task<ApiResponse<List<T>>> GetSpecialityAsync<T>(SpecilityMasterFilter filter) where T : class
        {
            ApiResponse<List<T>> response = new ApiResponse<List<T>>() { Success = true };
            try
            {
                DynamicParameters dbPara = new DynamicParameters();
                dbPara.Add("@Title", filter.Title, DbType.String);
                dbPara.Add("@OrganizationId", filter.OrganizationId, DbType.Int32);
                dbPara.Add("@IsActive", filter.IsActive, DbType.Boolean);
                response.Result = await _dapperHelper.GetAllAsync<T>(sp: SqlObjectName.SpecialityMasterSelect,
                                              parms: dbPara,
                                              commandType: CommandType.StoredProcedure);
            }
            catch (Exception ex)
            {
                response.ConstructErrorResponse("SpecialityMasterService GetProductAsync", ex.Message);
            }
            return response;
        }
        public async Task<ApiResponse<int>> CreateAsync(SpecilityMaster specilityMaster)
        {
            ApiResponse<int> response = new ApiResponse<int>() { Success = true };
            Validation validation = new Validation();
            validation.keys = new List<string>();

            using (TransactionScope transactionScope = new TransactionScope(TransactionScopeAsyncFlowOption.Enabled))
            {
                try
                {
                    if (string.IsNullOrEmpty(specilityMaster.Title))
                        validation.source = "Title";
                        validation.keys.Add("Title can not be allow null or empty.");

                    bool validationResponse = await ValidateNameAsync(specilityMaster.Title);
                    if (!validationResponse)
                        validation.source = "Title";
                        validation.keys.Add("Title is already exist.");

                    if (string.IsNullOrEmpty(specilityMaster.Description))
                        validation.source = "Description";
                        validation.keys.Add("Description can not be allow null or empty.");

                    if (specilityMaster.OrganizationId <= 0)
                        validation.source = "OrganizationId";
                        validation.keys.Add("OrganizationId can not be allow 0.");

                    DynamicParameters dbPara = new DynamicParameters();
                    dbPara.Add("@Title", specilityMaster.Title, DbType.String);
                    dbPara.Add("@SpecialityDescription", specilityMaster.Description, DbType.String);
                    dbPara.Add("@OrganizationId", specilityMaster.OrganizationId, DbType.Int32);
                    dbPara.Add("@IsActive", specilityMaster.IsActive, DbType.Boolean);
                    dbPara.Add("@InsertBy", specilityMaster.InsertdBy, DbType.String);
                    dbPara.Add("@InsertDate", specilityMaster.InsertedDate, DbType.DateTime);
                    response.Result = await _dapperHelper.GetAsync<int>(sp: SqlObjectName.SpecialityMasterInsert,
                                                  parms: dbPara,
                                                  commandType: CommandType.StoredProcedure);

                    transactionScope.Complete();
                }
                catch (Exception ex)
                {
                    response.ConstructErrorResponse("SpecialityMasterService CreateAsync", ex.Message);
                }
            }
            response.validation = validation;
            return response;
        }
        public async Task<ApiResponse<int>> DeleteAsync(int SpecialityId)
        {
            ApiResponse<int> response = new ApiResponse<int>() { Success = true };
            using (TransactionScope transactionScope = new TransactionScope(TransactionScopeAsyncFlowOption.Enabled))
            {
                try
                {
                    DynamicParameters dbPara = new DynamicParameters();
                    dbPara.Add("@SpecialityId", SpecialityId, DbType.Int32);
                    response.Result = await _dapperHelper.ExecuteAsync(sp: SqlObjectName.SpecialityMasterDelete,
                                                  parms: dbPara,
                                                  commandType: CommandType.StoredProcedure);

                    transactionScope.Complete();
                }
                catch (Exception ex)
                {
                    response.ConstructErrorResponse("SpecialityMasterService DeleteAsync", ex.Message);
                }

                return response;
            }
        }
        public async Task<ApiResponse<int>> UpdateAsync(SpecilityMaster specilityMaster)
        {

            ApiResponse<int> response = new ApiResponse<int>() { Success = true };
            Validation validation = new Validation();
            validation.keys = new List<string>();


            using (TransactionScope transactionScope = new TransactionScope(TransactionScopeAsyncFlowOption.Enabled))
            {
                try
                {
                    if (specilityMaster.SpecialityId == 0)
                        validation.source = "Title";
                    validation.keys.Add("Title can not be allow 0.");

                    if (string.IsNullOrEmpty(specilityMaster.Title))
                        validation.source = "Title";
                        validation.keys.Add("Title can not be allow null or empty.");

                    bool validationResponse = await ValidateAsync(specilityMaster.SpecialityId, specilityMaster.Title);
                    if (!validationResponse)
                        validation.source = "Title";
                        validation.keys.Add("Title is already exist.");

                    if (string.IsNullOrEmpty(specilityMaster.Description))
                        validation.source = "Description";
                        validation.keys.Add("Description can not be allow null or empty.");

                    if (specilityMaster.OrganizationId <= 0)
                        validation.source = "OrganizationId";
                        validation.keys.Add("OrganizationId can not be allow 0.");

                    DynamicParameters dbPara = new DynamicParameters();

                    dbPara.Add("@SpecialityId", specilityMaster.SpecialityId, DbType.Int32);

                    dbPara.Add("@Title", specilityMaster.Title, DbType.String);
                    dbPara.Add("@SpecialityDescription", specilityMaster.Description, DbType.String);
                    dbPara.Add("@OrganizationId", specilityMaster.OrganizationId, DbType.Int32);
                    dbPara.Add("@IsActive", specilityMaster.IsActive, DbType.Boolean);
                    dbPara.Add("@UpdateBy", specilityMaster.UpdatedBy, DbType.String);

                    dbPara.Add("@UpdateDate", specilityMaster.UpdateDate, DbType.DateTime);

                    dbPara.Add("@UpdatedDate", specilityMaster.UpdateDate, DbType.DateTime);

                    response.Result = await _dapperHelper.GetAsync<int>(sp: SqlObjectName.SpecialityMasterUpdate,
                                                  parms: dbPara,
                                                  commandType: CommandType.StoredProcedure);

                    transactionScope.Complete();
                }
                catch (Exception ex)
                {
                    response.ConstructErrorResponse("SpecialityMasterService UpdateAsync", ex.Message);
                }

            }
            response.validation = validation;
            return response;
        }
        public async Task<bool> ValidateAsync(int SpecialityId, string Title)
        {
            DynamicParameters dbPara = new DynamicParameters();
            dbPara.Add("@SpecialityId", SpecialityId, DbType.Int32);
            dbPara.Add("@Title", Title, DbType.String);
            bool response = await _dapperHelper.GetAsync<bool>(sp: SqlObjectName.SpecialityMasterValidateTitle,
                                            parms: dbPara,
                                            commandType: CommandType.StoredProcedure);
            return response;
        }

        public async Task<bool> ValidateNameAsync(string Title)
        {
            DynamicParameters dbPara = new DynamicParameters();
            dbPara.Add("@Title", Title, DbType.String);

            bool response = await _dapperHelper.GetAsync<bool>(sp: SqlObjectName.SpecialityMasterValidateExistsTitle,

                                            parms: dbPara,
                                            commandType: CommandType.StoredProcedure);
            return response;
        }
    }
}
