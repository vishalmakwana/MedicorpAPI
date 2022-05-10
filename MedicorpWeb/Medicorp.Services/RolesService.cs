using Medicorp.Core;
using Medicorp.Core.Entity.Master;
using Medicorp.IServices;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Medicorp.Services
{
    public class RolesService : IRolesServicecs
    {
        Task<ApiResponse<List<AspNetRoles>>> IRolesServicecs.GetOrganizationAsync(AspNetRolesFilter filter)
        {
            throw new NotImplementedException();
        }

        Task<ApiResponse<List<T>>> IRolesServicecs.GetOrganizationAsync<T>(AspNetRolesFilter filter)
        {
            throw new NotImplementedException();
        }
    }
}
