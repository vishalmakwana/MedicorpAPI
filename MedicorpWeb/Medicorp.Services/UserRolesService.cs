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
    public class UserRolesService : IUserRolesService
    {
        Task<ApiResponse<List<UserRoles>>> IUserRolesService.GetUserRolesAsync(UserRolesFilter filter)
        {
            throw new NotImplementedException();
        }

        Task<ApiResponse<List<T>>> IUserRolesService.GetUserRolesAsync<T>(UserRolesFilter filter)
        {
            throw new NotImplementedException();
        }
    }
}
