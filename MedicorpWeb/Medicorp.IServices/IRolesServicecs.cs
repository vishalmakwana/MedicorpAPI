using Medicorp.Core;
using Medicorp.Core.Entity.Master;

namespace Medicorp.IServices
{
    public interface IRolesServicecs
    {
        Task<ApiResponse<List<AspNetRoles>>> GetOrganizationAsync(AspNetRolesFilter filter);
        Task<ApiResponse<List<T>>> GetOrganizationAsync<T>(AspNetRolesFilter filter) where T : class;
    }
}
