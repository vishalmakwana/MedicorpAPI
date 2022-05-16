using Dapper;
using Medicorp.Core;
using Medicorp.Core.CustomException;
using Medicorp.Core.Entity.Master;
using Medicorp.Data.InterFace;
using Medicorp.IServices;
using System.Data;
using System.Transactions;

namespace Medicorp.Services
{
    public class CategoryMasterService : ICategoryMasterService
    {
        private readonly IDapperHelper _dapperHelper;
        public CategoryMasterService(IDapperHelper dapperHelper)
        {
            _dapperHelper = dapperHelper;
        }

        public async Task<ApiResponse<int>> CreateAsync(CategoryMaster categoryMaster)
        {
            ApiResponse<int> response = new ApiResponse<int>() { Success = true };
            using (TransactionScope transactionScope = new TransactionScope(TransactionScopeAsyncFlowOption.Enabled))
            {
                try
                {
                    if (string.IsNullOrEmpty(categoryMaster.CategoryName))
                        throw new OperationExecutionException("Category Name is not valid");
                    bool validationResponse = await ValidateNameAsync(categoryMaster.CategoryName);
                    if (!validationResponse)
                        throw new OperationExecutionException("Category Name is already exists");

                    DynamicParameters dbPara = new DynamicParameters();
                    dbPara.Add("@CategoryName", categoryMaster.CategoryName, DbType.String);
                    dbPara.Add("@OrganizationId", categoryMaster.OrganizationId, DbType.Int32);
                    dbPara.Add("@IsActive", categoryMaster.IsActive, DbType.Boolean);
                    dbPara.Add("@InsertBy", categoryMaster.InsertdBy, DbType.String);
                    dbPara.Add("@InsertDate", categoryMaster.InsertedDate, DbType.DateTime);
                    response.Result = await _dapperHelper.GetAsync<int>(sp: SqlObjectName.CategoryMasterInsert,
                                                  parms: dbPara,
                                                  commandType: CommandType.StoredProcedure);

                    transactionScope.Complete();
                }
                catch (Exception ex)
                {
                    response.ConstructErrorResponse("CategoryMasterService CreateAsync", ex.Message);
                }
            }
            return response;
        }

        

        public async Task<ApiResponse<List<CategoryMaster>>>GetCategoryAsync(CategoryMasterFilter filter)
        {
            return await GetCategoryAsync<CategoryMaster>(filter);
        }

        public async Task<ApiResponse<List<T>>> GetCategoryAsync<T>(CategoryMasterFilter filter) where T : class
        {
            ApiResponse<List<T>> response = new ApiResponse<List<T>>() { Success = true };
            try
            {
                DynamicParameters dbPara = new DynamicParameters();
                dbPara.Add("@CategoryId", filter.CategoryId, DbType.Int32);
                dbPara.Add("@CategoryName", filter.CategoryName, DbType.String);
                dbPara.Add("@OrganizationId", filter.OrganizationId, DbType.Int32);
                dbPara.Add("@IsActive", filter.IsActive, DbType.Boolean);
                response.Result = await _dapperHelper.GetAllAsync<T>(sp: SqlObjectName.CategoryMasterSelect,
                                              parms: dbPara,
                                              commandType: CommandType.StoredProcedure);
            }
            catch (Exception ex)
            {
                response.ConstructErrorResponse("CategoryMasterService GetProductAsync", ex.Message);
            }
            return response;
        }
        public async Task<ApiResponse<int>> UpdateAsync(CategoryMaster categoryMaster)
        {

            ApiResponse<int> response = new ApiResponse<int>() { Success = true };
            using (TransactionScope transactionScope = new TransactionScope(TransactionScopeAsyncFlowOption.Enabled))
            {
                try
                {
                    if (categoryMaster.CategoryId == 0)
                        throw new OperationExecutionException("Category Id is not valid");
                    if (string.IsNullOrEmpty(categoryMaster.CategoryName))
                        throw new OperationExecutionException("Category name is not valid");
                    bool validationResponse = await ValidateAsync(categoryMaster.CategoryId, categoryMaster.CategoryName);
                    if (!validationResponse)
                        throw new OperationExecutionException("Category name is already exists");

                    DynamicParameters dbPara = new DynamicParameters();
                    dbPara.Add("@CategoryName", categoryMaster.CategoryName, DbType.String);
                    dbPara.Add("@OrganizationId", categoryMaster.OrganizationId, DbType.Int32);
                    dbPara.Add("@IsActive", categoryMaster.IsActive, DbType.Boolean);
                    dbPara.Add("@UpdateBy", categoryMaster.UpdatedBy, DbType.String);
                    dbPara.Add("@UpdateDate", categoryMaster.UpdateDate, DbType.DateTime);
                    response.Result = await _dapperHelper.GetAsync<int>(sp: SqlObjectName.CategoryMasterUpdate,
                                                  parms: dbPara,
                                                  commandType: CommandType.StoredProcedure);

                    transactionScope.Complete();
                }
                catch (Exception ex)
                {
                    response.ConstructErrorResponse("CategoryMasterService UpdateAsync", ex.Message);
                }

            }
            return response;
        }
        public async Task<ApiResponse<int>> DeleteAsync(int categoryId)
        {
            ApiResponse<int> response = new ApiResponse<int>() { Success = true };
            using (TransactionScope transactionScope = new TransactionScope(TransactionScopeAsyncFlowOption.Enabled))
            {
                try
                {
                    DynamicParameters dbPara = new DynamicParameters();
                    dbPara.Add("@CategoryId", categoryId, DbType.Int32);
                    response.Result = await _dapperHelper.ExecuteAsync(sp: SqlObjectName.CategoryMasterDelete,
                                                  parms: dbPara,
                                                  commandType: CommandType.StoredProcedure);

                    transactionScope.Complete();
                }
                catch (Exception ex)
                {
                    response.ConstructErrorResponse("CategoryMasterService DeleteAsync", ex.Message);
                }

                return response;
            }
        }
        public async Task<ApiResponse<int>> GetAsync(int id)
        {
            ApiResponse<int> response = new ApiResponse<int>() { Success = true };
            using (TransactionScope transactionScope = new TransactionScope(TransactionScopeAsyncFlowOption.Enabled))
            {
                try
                {
                    DynamicParameters dbPara = new DynamicParameters();
                    dbPara.Add("@CategoryId", id, DbType.Int32);
                    response.Result = await _dapperHelper.ExecuteAsync(sp: SqlObjectName.CategoryMasterSelect,
                                                  parms: dbPara,
                                                  commandType: CommandType.StoredProcedure);

                    transactionScope.Complete();
                }
                catch (Exception ex)
                {
                    response.ConstructErrorResponse("CategoryMasterService DeleteAsync", ex.Message);
                }

                return response;
            }
        }
        public async Task<bool> ValidateAsync(int CategoryId, string CategoryName)
        {
            DynamicParameters dbPara = new DynamicParameters();
            dbPara.Add("@CategoryId", CategoryId, DbType.Int32);
            dbPara.Add("@CategoryName", CategoryName, DbType.String);
            bool response = await _dapperHelper.GetAsync<bool>(sp: SqlObjectName.CategoryMasterValidateCategoryName,
                                            parms: dbPara,
                                            commandType: CommandType.StoredProcedure);
            return response;
        }

        public async Task<bool> ValidateNameAsync(string CategoryName)
        {
            DynamicParameters dbPara = new DynamicParameters();
            dbPara.Add("@CategoryName", CategoryName, DbType.String);
            bool response = await _dapperHelper.GetAsync<bool>(sp: SqlObjectName.CategoryMasterExistCategoryName,
                                            parms: dbPara,
                                            commandType: CommandType.StoredProcedure);
            return response;
        }
    }
}
