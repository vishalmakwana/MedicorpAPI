using Dapper;
using Medicorp.Core;
using Medicorp.Core.Entity.Master;
using Medicorp.Data.InterFace;
using Medicorp.IServices;
using System.Data;


namespace Medicorp.Services
{
    public class UserRolesService : IUserRolesService
    {
        private readonly IDapperHelper _dapperHelper;
        public UserRolesService(IDapperHelper dapperHelper)
        {
            _dapperHelper = dapperHelper;
        }
        async Task<ApiResponse<List<UserRoles>>> IUserRolesService.GetUserRolesAsync(UserRolesFilter filter)
        {
            return await GetUserRolesAsync<UserRoles>(filter);
        }

        public async Task<ApiResponse<List<T>>> GetUserRolesAsync<T>(UserRolesFilter filter) where T : class
        {
            ApiResponse<List<T>> response = new ApiResponse<List<T>>() { Success = true };
            try
            {
                DynamicParameters dbPara = new DynamicParameters();
                dbPara.Add("@UserId", filter.UserId, DbType.String);
                dbPara.Add("@UserName", filter.UserName, DbType.String);
                dbPara.Add("@RoleId", filter.RoleId, DbType.String);
                dbPara.Add("@RoleName", filter.RoleName, DbType.String);

                response.Result = await _dapperHelper.GetAllAsync<T>(sp: SqlObjectName.UserRolesMasterSelect,
                                              parms: dbPara,
                                              commandType: CommandType.StoredProcedure);
            }
            catch (Exception ex)
            {
                response.ConstructErrorResponse("UserRolesService GetUserRolesAsync", ex.Message);
            }
            return response;
        }
    }
}
