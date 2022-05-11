using Medicorp.Core;
using Medicorp.Core.Entity.Master;

namespace Medicorp.IServices
{
    public interface IRolesServicecs
    {
        Task<ApiResponse<List<AspNetRoles>>> GetRolesAsync(AspNetRolesFilter filter);
        Task<ApiResponse<List<T>>> GetRolesAsync<T>(AspNetRolesFilter filter) where T : class;
    }
}
