using Medicorp.Core;
using Medicorp.Core.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Medicorp.IServices
{
    public interface IOrganizationMaster
    {
        Task<ApiResponse<List<OrganizationMaster>>> GetOrganizationAsync(OrganizationMasterFilter filter);
        Task<ApiResponse<List<T>>> GetOrganizationAsync<T>(OrganizationMasterFilter filter) where T : class;
        Task<ApiResponse<int>> UpdateAsync(OrganizationMaster organizationMaster);
        Task<ApiResponse<int>> CreateAsync(OrganizationMaster organizationMaster);
        Task<ApiResponse<int>> DeleteAsync(int OrganizationId);
        Task<bool> ValidateAsync(int OrganizationId, string OrganizationName);
        Task<bool> ValidateNameAsync(string OrganizationName);
    }
}
