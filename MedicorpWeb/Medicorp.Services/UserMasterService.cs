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
            Validation validation = new Validation();
            validation.keys = new List<string>();

            using (TransactionScope transactionScope = new TransactionScope(TransactionScopeAsyncFlowOption.Enabled))
            {
                try
                {
                    if (string.IsNullOrEmpty(userMaster.Username))
                        validation.source = "Username";
                        validation.keys.Add("Username can not be allow null or empty.");

                    bool validationResponse = await ValidateNameAsync(userMaster.Username);
                    if (!validationResponse)
                        validation.source = "Username";
                        validation.keys.Add("Username  is already exist.");

                    if (string.IsNullOrEmpty(userMaster.Firstname))
                        validation.source = "FirstName";
                        validation.keys.Add("FirstName can not be allow null or empty.");

                    if (string.IsNullOrEmpty(userMaster.Lastname))
                        validation.source = "LastName";
                        validation.keys.Add("LastName can not be allow null or empty.");

                    if (string.IsNullOrEmpty(userMaster.Email))
                        validation.source = "Email";
                        validation.keys.Add("Email can not be allow null or empty.");

                    if (string.IsNullOrEmpty(userMaster.PhoneNumber))
                        validation.source = "PhoneNumber";
                        validation.keys.Add("PhoneNumber can not be allow null or empty.");

                    if (userMaster.PhoneNumber.Length < 10 || userMaster.PhoneNumber.Length > 10)
                        validation.source = "PhoneNumber";
                        validation.keys.Add("PhoneNumber maximum length valid 10 digit");

                    if (userMaster.OrganizationId <= 0)
                        validation.source = "OrganizationId";
                        validation.keys.Add("OrganizationId can not be allow 0.");

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
            response.validation = validation;
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
                response.ConstructErrorResponse("UserMasterService GetUserAsync", ex.Message);
            }
            return response;
        }

        public async Task<ApiResponse<int>> UpdateAsync(UserMaster userMaster)
        {
            ApiResponse<int> response = new ApiResponse<int>() { Success = true };
            Validation validation = new Validation();
            validation.keys = new List<string>();

            using (TransactionScope transactionScope = new TransactionScope(TransactionScopeAsyncFlowOption.Enabled))
            {
                try
                {
                    if (userMaster.Id == "")
                        validation.source = "Id";
                        validation.keys.Add("Id can not be allow 0.");

                    if (string.IsNullOrEmpty(userMaster.Username))
                        validation.source = "Username";
                        validation.keys.Add("Username can not be allow null or empty.");

                    bool validationResponse = await ValidateAsync(userMaster.Id, userMaster.Username);
                    if (!validationResponse)
                        validation.source = "Username";
                        validation.keys.Add("Username can not be allow null or empty.");


                    if (string.IsNullOrEmpty(userMaster.Firstname))
                        validation.source = "FirstName";
                        validation.keys.Add("FirstName can not be allow null or empty.");

                    if (string.IsNullOrEmpty(userMaster.Lastname))
                        validation.source = "LastName";
                        validation.keys.Add("LastName can not be allow null or empty.");

                    if (string.IsNullOrEmpty(userMaster.Email))
                        validation.source = "Email";
                        validation.keys.Add("Email can not be allow null or empty.");

                    if (string.IsNullOrEmpty(userMaster.PhoneNumber))
                        validation.source = "PhoneNumber";
                        validation.keys.Add("PhoneNumber can not be allow null or empty.");

                    if (userMaster.PhoneNumber.Length < 10 || userMaster.PhoneNumber.Length > 10)
                        validation.source = "PhoneNumber";
                        validation.keys.Add("PhoneNumber maximum length valid 10 digit");

                    if (userMaster.OrganizationId <= 0)
                        validation.source = "OrganizationId";
                        validation.keys.Add("OrganizationId can not be allow 0.");

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
            response.validation = validation;
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