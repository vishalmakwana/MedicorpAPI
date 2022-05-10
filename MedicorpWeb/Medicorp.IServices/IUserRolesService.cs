using Medicorp.Core;
using Medicorp.Core.Entity.Master;


namespace Medicorp.IServices
{
    public interface IUserRolesService
    {
        Task<ApiResponse<List<UserRoles>>> GetUserRolesAsync(UserRolesFilter filter);
        Task<ApiResponse<List<T>>> GetUserRolesAsync<T>(UserRolesFilter filter) where T : class;
    }
}
