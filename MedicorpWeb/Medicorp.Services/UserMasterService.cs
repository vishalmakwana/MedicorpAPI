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
    public class UserMasterService : IUserMasterService
    {

        private readonly IDapperHelper _dapperHelper;
        public UserMasterService(IDapperHelper dapperHelper)
        {
            _dapperHelper = dapperHelper;
        }

        public async Task<ApiResponse<int>> CreateAsync(UserMaster userMaster)
        {
            ApiResponse<int> response = new ApiResponse<int>() { Success = true };
            using (TransactionScope transactionScope = new TransactionScope(TransactionScopeAsyncFlowOption.Enabled))
            {
                try
                {
                    if (string.IsNullOrEmpty(userMaster.Username))
                        throw new OperationExecutionException("User Name is not valid");
                    bool validationResponse = await ValidateNameAsync(userMaster.Username);
                    if (!validationResponse)
                        throw new OperationExecutionException("User Name is already exists");

                    DynamicParameters dbPara = new DynamicParameters();
                    dbPara.Add("@Username", userMaster.Username, DbType.String);
                    dbPara.Add("@Firstname", userMaster.Firstname, DbType.String);
                    dbPara.Add("@Lastname", userMaster.Lastname, DbType.String);
                    dbPara.Add("@Email", userMaster.Email, DbType.String);
                    dbPara.Add("@PhoneNumber", userMaster.PhoneNumber, DbType.String);
                    dbPara.Add("@OrganizationId", userMaster.OrganizationId, DbType.Int32);
                    dbPara.Add("@IsActive", userMaster.IsActive, DbType.Boolean);
                    dbPara.Add("@EmailConfirmed", userMaster.EmailConfirmed, DbType.Boolean);
                    dbPara.Add("@PhoneNumberConfirmed", userMaster.PhoneNumberConfirmed, DbType.Boolean);
                    
                    response.Result = await _dapperHelper.GetAsync<int>(sp: SqlObjectName.UserMasterInsert,
                                                  parms: dbPara,
                                                  commandType: CommandType.StoredProcedure);

                    transactionScope.Complete();
                }
                catch (Exception ex)
                {
                    response.ConstructErrorResponse("UserMasterService CreateAsync", ex.Message);
                }
            }
            return response;
        }

        public async Task<ApiResponse<int>> DeleteAsync(int UserId)
        {
            ApiResponse<int> response = new ApiResponse<int>() { Success = true };
            using (TransactionScope transactionScope = new TransactionScope(TransactionScopeAsyncFlowOption.Enabled))
            {
                try
                {
                    DynamicParameters dbPara = new DynamicParameters();
                    dbPara.Add("@UserId", UserId, DbType.Int32);
                    response.Result = await _dapperHelper.ExecuteAsync(sp: SqlObjectName.UserMasterDelete,
                                                  parms: dbPara,
                                                  commandType: CommandType.StoredProcedure);

                    transactionScope.Complete();
                }
                catch (Exception ex)
                {
                    response.ConstructErrorResponse("UserMasterService DeleteAsync", ex.Message);
                }

                return response;
            }
        }

        public async Task<ApiResponse<List<UserMaster>>> GetUserAsync(UserMasterFilter filter)
        {
            return await GetUserAsync<UserMaster>(filter);
        }

        public async Task<ApiResponse<List<T>>> GetUserAsync<T>(UserMasterFilter filter) where T : class
        {
            ApiResponse<List<T>> response = new ApiResponse<List<T>>() { Success = true };
            try
            {
                DynamicParameters dbPara = new DynamicParameters();
                dbPara.Add("@Username", filter.Username, DbType.String);
                dbPara.Add("@Firstname", filter.Firstname, DbType.String);
                dbPara.Add("@Lastname", filter.Lastname, DbType.String);
                dbPara.Add("@Email", filter.Email, DbType.String);
                dbPara.Add("@PhoneNumber", filter.PhoneNumber, DbType.String);
                dbPara.Add("@OrganizationId", filter.OrganizationId, DbType.Int32);
                dbPara.Add("@IsActive", filter.IsActive, DbType.Boolean);
                dbPara.Add("@EmailConfirmed", filter.EmailConfirmed, DbType.Boolean);
                dbPara.Add("@PhoneNumberConfirmed", filter.PhoneNumberConfirmed, DbType.Boolean);
                dbPara.Add("@OrganizationName", filter.OrganizationName, DbType.Boolean);
                response.Result = await _dapperHelper.GetAllAsync<T>(sp: SqlObjectName.UserMasterSelect,
                                              parms: dbPara,
                                              commandType: CommandType.StoredProcedure);
            }
            catch (Exception ex)
            {
                response.ConstructErrorResponse("UserMasterService GetProductAsync", ex.Message);
            }
            return response;
        }

        public async Task<ApiResponse<int>> UpdateAsync(UserMaster userMaster)
        {
            ApiResponse<int> response = new ApiResponse<int>() { Success = true };
            using (TransactionScope transactionScope = new TransactionScope(TransactionScopeAsyncFlowOption.Enabled))
            {
                try
                {
                    if (userMaster.Id == "")
                        throw new OperationExecutionException("User Id is not valid");
                    if (string.IsNullOrEmpty(userMaster.Username))
                        throw new OperationExecutionException("User name is not valid");
                    bool validationResponse = await ValidateAsync(userMaster.Id, userMaster.Username);
                    if (!validationResponse)
                        throw new OperationExecutionException("Product name is already exists");

                    DynamicParameters dbPara = new DynamicParameters();
                    dbPara.Add("@Username", userMaster.Username, DbType.String);
                    dbPara.Add("@Firstname", userMaster.Firstname, DbType.String);
                    dbPara.Add("@Lastname", userMaster.Lastname, DbType.String);
                    dbPara.Add("@Email", userMaster.Email, DbType.String);
                    dbPara.Add("@PhoneNumber", userMaster.PhoneNumber, DbType.String);
                    dbPara.Add("@OrganizationId", userMaster.OrganizationId, DbType.Int32);
                    dbPara.Add("@IsActive", userMaster.IsActive, DbType.Boolean);
                    dbPara.Add("@EmailConfirmed", userMaster.EmailConfirmed, DbType.Boolean);
                    dbPara.Add("@PhoneNumberConfirmed", userMaster.PhoneNumberConfirmed, DbType.Boolean);
                    response.Result = await _dapperHelper.GetAsync<int>(sp: SqlObjectName.UserMasterUpdate,
                                                  parms: dbPara,
                                                  commandType: CommandType.StoredProcedure);

                    transactionScope.Complete();
                }
                catch (Exception ex)
                {
                    response.ConstructErrorResponse("UserMasterService UpdateAsync", ex.Message);
                }

            }
            return response;
        }

        public async Task<bool> ValidateAsync(string userId, string userName)
        {
            DynamicParameters dbPara = new DynamicParameters();
            dbPara.Add("@UserId", userId, DbType.Int32);
            dbPara.Add("@UserName", userName, DbType.String);
            bool response = await _dapperHelper.GetAsync<bool>(sp: SqlObjectName.UserMasterValidateUserName,
                                            parms: dbPara,
                                            commandType: CommandType.StoredProcedure);
            return response;
        }

        public async Task<bool> ValidateNameAsync(string userName)
        {
            DynamicParameters dbPara = new DynamicParameters();
            dbPara.Add("@userName", userName, DbType.String);
            bool response = await _dapperHelper.GetAsync<bool>(sp: SqlObjectName.UserMasterExistUserName,
                                            parms: dbPara,
                                            commandType: CommandType.StoredProcedure);
            return response;
        }
    }
}