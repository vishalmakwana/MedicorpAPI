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
    public class DoctorMasterServices : IDoctorMasterServices
    {
        private readonly IDapperHelper _dapperHelper;
        public DoctorMasterServices(IDapperHelper dapperHelper)
        {
            _dapperHelper = dapperHelper;
        }
        async Task<ApiResponse<int>> IDoctorMasterServices.CreateAsync(DoctorMaster doctorMaster)
        {
            ApiResponse<int> response = new ApiResponse<int>() { Success = true };
            Validation validation = new Validation();
            validation.keys = new List<string>();

            using (TransactionScope transactionScope = new TransactionScope(TransactionScopeAsyncFlowOption.Enabled))
            {
                try
                {
                    if (string.IsNullOrEmpty(doctorMaster.FirstName))
                        validation.source = "FirstName";
                        validation.keys.Add("FirstName can not be allow null or empty.");

                    if (string.IsNullOrEmpty(doctorMaster.LastName))
                        validation.source = "LastName";
                        validation.keys.Add("LastName can not be allow null or empty.");

                    if (string.IsNullOrEmpty(doctorMaster.Email))
                        validation.source = "Email";
                        validation.keys.Add("Email can not be allow null or empty.");

                    bool validationResponse = await ValidateNameAsync(doctorMaster.Email);
                    if (!validationResponse)
                        validation.source = "Email";
                        validation.keys.Add("Email Name is already exist.");

                    if (doctorMaster.StateId <= 0)
                        validation.source = "StateId";
                        validation.keys.Add("StateId can not be allow 0.");

                    if (doctorMaster.CityId <= 0)
                        validation.source = "StateId";
                        validation.keys.Add("CityId can not be allow 0.");

                    if (string.IsNullOrEmpty(doctorMaster.Address))
                        validation.source = "Address";
                        validation.keys.Add("Address can not be allow null or empty.");

                    if (string.IsNullOrEmpty(doctorMaster.Mobilenumber))
                        validation.source = "Mobilenumber";
                        validation.keys.Add("Mobilenumber can not be allow null or empty.");

                    if (doctorMaster.Mobilenumber.Length<10 || doctorMaster.Mobilenumber.Length>10)
                        validation.source = "Mobilenumber";
                        validation.keys.Add("Mobilenumber maximum length valid 10 digit");

                    if (string.IsNullOrEmpty(doctorMaster.Gender))
                        validation.source = "Gender";
                        validation.keys.Add("Gender can not be allow null or empty.");

                    if (doctorMaster.OrganizationId <= 0)
                        validation.source = "OrganizationId";
                        validation.keys.Add("OrganizationId can not be allow 0.");

                    DynamicParameters dbPara = new DynamicParameters();
                    dbPara.Add("@FirstName", doctorMaster.FirstName, DbType.String);
                    dbPara.Add("@LastName", doctorMaster.LastName, DbType.String);
                    dbPara.Add("@Gender", doctorMaster.Gender, DbType.String);
                    dbPara.Add("@Email", doctorMaster.Email, DbType.String);
                    dbPara.Add("@Addresses", doctorMaster.Address, DbType.String);
                    dbPara.Add("@CityId", doctorMaster.CityId, DbType.Int32);
                    dbPara.Add("@StateId", doctorMaster.StateId, DbType.Int32);
                    dbPara.Add("@MobileNumber", doctorMaster.Mobilenumber, DbType.String);
                    dbPara.Add("@OrganizationId", doctorMaster.OrganizationId, DbType.Int32);
                    dbPara.Add("@IsActive", doctorMaster.IsActive, DbType.Boolean);
                    dbPara.Add("@InsertBy", doctorMaster.InsertdBy, DbType.String);
                    dbPara.Add("@InsertDate", doctorMaster.InsertedDate, DbType.DateTime);
                    response.Result = await _dapperHelper.GetAsync<int>(sp: SqlObjectName.DoctorInsert,
                                                  parms: dbPara,
                                                  commandType: CommandType.StoredProcedure);

                    transactionScope.Complete();
                }
                catch (Exception ex)
                {
                    response.ConstructErrorResponse("DoctorMasterService CreateAsync", ex.Message);
                }
            }
            response.validation = validation;
            return response;
        }

        async Task<ApiResponse<int>> IDoctorMasterServices.DeleteAsync(int DoctorId)
        {
            ApiResponse<int> response = new ApiResponse<int>() { Success = true };
            using (TransactionScope transactionScope = new TransactionScope(TransactionScopeAsyncFlowOption.Enabled))
            {
                try
                {
                    DynamicParameters dbPara = new DynamicParameters();
                    dbPara.Add("@DoctorId", DoctorId, DbType.Int32);
                    response.Result = await _dapperHelper.ExecuteAsync(sp: SqlObjectName.DoctorDelete,
                                                  parms: dbPara,
                                                  commandType: CommandType.StoredProcedure);

                    transactionScope.Complete();
                }
                catch (Exception ex)
                {
                    response.ConstructErrorResponse("DoctorMasterService DeleteAsync", ex.Message);
                }

                return response;
            }
        }

        async Task<ApiResponse<List<DoctorMaster>>> IDoctorMasterServices.GetDoctorAsync(DoctorMasterFilter filter)
        {
            return await GetDoctorAsync<DoctorMaster>(filter);
        }
        public async Task<ApiResponse<List<T>>> GetDoctorAsync<T>(DoctorMasterFilter filter) where T : class
        {
            ApiResponse<List<T>> response = new ApiResponse<List<T>>() { Success = true };
            try
            {
                DynamicParameters dbPara = new DynamicParameters();
                dbPara.Add("@FirstName", filter.FirstName, DbType.String);
                dbPara.Add("@LastName", filter.LastName, DbType.String);
                dbPara.Add("@Gender", filter.Gender, DbType.String);
                dbPara.Add("@Email", filter.Email, DbType.String);
                dbPara.Add("@Addresses", filter.Address, DbType.String);
                dbPara.Add("@CityId", filter.CityId, DbType.Int32);
                dbPara.Add("@StateId", filter.StateId, DbType.Int32);
                dbPara.Add("@MobileNumber", filter.Mobilenumber, DbType.String);
                dbPara.Add("@OrganizationId", filter.OrganizationId, DbType.Int32);
                dbPara.Add("@IsActive", filter.IsActive, DbType.Boolean);
                response.Result = await _dapperHelper.GetAllAsync<T>(sp: SqlObjectName.DoctorSelect,
                                              parms: dbPara,
                                              commandType: CommandType.StoredProcedure);
            }
            catch (Exception ex)
            {
                response.ConstructErrorResponse("DoctorMasterService GetProductAsync", ex.Message);
            }
            return response;
        }

        async Task<ApiResponse<int>> IDoctorMasterServices.UpdateAsync(DoctorMaster doctorMaster)
        {
            ApiResponse<int> response = new ApiResponse<int>() { Success = true };
            Validation validation = new Validation();
            validation.keys = new List<string>();

            using (TransactionScope transactionScope = new TransactionScope(TransactionScopeAsyncFlowOption.Enabled))
            {
                try
                {
                    if (doctorMaster.DoctorId == 0)
                        validation.source = "DoctorId";
                        validation.keys.Add("DoctorId can not be allow 0.");

                    if (string.IsNullOrEmpty(doctorMaster.FirstName))
                        validation.source = "FirstName";
                        validation.keys.Add("FirstName can not be allow null or empty.");

                    if (string.IsNullOrEmpty(doctorMaster.LastName))
                        validation.source = "LastName";
                        validation.keys.Add("LastName can not be allow null or empty.");

                    if (string.IsNullOrEmpty(doctorMaster.Email))
                        validation.source = "Email";
                        validation.keys.Add("Email can not be allow null or empty.");

                    bool validationResponse = await ValidateAsync(doctorMaster.DoctorId, doctorMaster.Email);
                    if (!validationResponse)
                        validation.source = "Email";
                        validation.keys.Add("Email Name is already exist.");

                    if (doctorMaster.StateId <= 0)
                        validation.source = "StateId";
                        validation.keys.Add("StateId can not be allow 0.");

                    if (doctorMaster.CityId <= 0)
                        validation.source = "StateId";
                        validation.keys.Add("CityId can not be allow 0.");

                    if (string.IsNullOrEmpty(doctorMaster.Address))
                        validation.source = "Address";
                        validation.keys.Add("Address can not be allow null or empty.");

                    if (string.IsNullOrEmpty(doctorMaster.Mobilenumber))
                        validation.source = "Mobilenumber";
                        validation.keys.Add("Mobilenumber can not be allow null or empty.");

                    if (doctorMaster.Mobilenumber.Length < 10 || doctorMaster.Mobilenumber.Length > 10)
                        validation.source = "Mobilenumber";
                        validation.keys.Add("Mobilenumber maximum length valid 10 digit");

                    if (string.IsNullOrEmpty(doctorMaster.Gender))
                        validation.source = "Gender";
                        validation.keys.Add("Gender can not be allow null or empty.");

                    if (doctorMaster.OrganizationId <= 0)
                        validation.source = "OrganizationId Id";
                        validation.keys.Add("OrganizationId can not be allow 0.");

                    DynamicParameters dbPara = new DynamicParameters();
                    dbPara.Add("@DoctorId", doctorMaster.DoctorId, DbType.Int32);
                    dbPara.Add("@FirstName", doctorMaster.FirstName, DbType.String);
                    dbPara.Add("@LastName", doctorMaster.LastName, DbType.String);
                    dbPara.Add("@Gender", doctorMaster.Gender, DbType.String);
                    dbPara.Add("@Email", doctorMaster.Email, DbType.String);
                    dbPara.Add("@Addresses", doctorMaster.Address, DbType.String);
                    dbPara.Add("@CityId", doctorMaster.CityId, DbType.Int32);
                    dbPara.Add("@StateId", doctorMaster.StateId, DbType.Int32);
                    dbPara.Add("@MobileNumber", doctorMaster.Mobilenumber, DbType.String);
                    dbPara.Add("@OrganizationId", doctorMaster.OrganizationId, DbType.Int32);
                    dbPara.Add("@IsActive", doctorMaster.IsActive, DbType.Boolean);
                    dbPara.Add("@UpdateBy", doctorMaster.UpdatedBy, DbType.String);
                    dbPara.Add("@UpdateDate", doctorMaster.UpdateDate, DbType.DateTime);
                    response.Result = await _dapperHelper.GetAsync<int>(sp: SqlObjectName.DoctorUpdate,
                    
                                                  parms: dbPara,
                                                  commandType: CommandType.StoredProcedure);

                    transactionScope.Complete();
                }
                catch (Exception ex)
                {
                    response.ConstructErrorResponse("DoctorMasterService UpdateAsync", ex.Message);
                }

            }
            response.validation = validation;
            return response;
        }

        public async Task<bool> ValidateAsync(int DoctorId, string Email)
        {
            DynamicParameters dbPara = new DynamicParameters();
            dbPara.Add("@DoctorId", DoctorId, DbType.Int32);
            dbPara.Add("@Email", Email, DbType.String);
            bool response = await _dapperHelper.GetAsync<bool>(sp: SqlObjectName.DoctorMasterValidateEmail,
                                            parms: dbPara,
                                            commandType: CommandType.StoredProcedure);
            return response;
        }

        public async Task<bool> ValidateNameAsync(string Email)
        {
            DynamicParameters dbPara = new DynamicParameters();
            dbPara.Add("@Email", Email, DbType.String);
            bool response = await _dapperHelper.GetAsync<bool>(sp: SqlObjectName.DoctorMasterValidateExistsEmail,
                                            parms: dbPara,
                                            commandType: CommandType.StoredProcedure);
            return response;
        }
    }
}
