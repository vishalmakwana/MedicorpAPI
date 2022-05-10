using Medicorp.Core;
using Medicorp.Core.Entity;
using Medicorp.IServices;

namespace Medicorp.Services
{
    public class UserMasterService : IUserMasterService
    {
        public Task<ApiResponse<int>> CreateAsync(int UserId, List<UserMaster> userMaster)
        {
            throw new NotImplementedException();
        }

        public Task<ApiResponse<int>> DeleteAsync(int UserId)
        {
            throw new NotImplementedException();
        }

        public Task<List<UserMaster>> GetUser(UserMasterFilter filter)
        {
            throw new NotImplementedException();
        }

        public Task<ApiResponse<List<UserMaster>>> GetUserAsync(UserMasterFilter filter)
        {
            throw new NotImplementedException();
        }

        public Task<ApiResponse<List<T>>> GetUserAsync<T>(UserMasterFilter filter) where T : class
        {
            throw new NotImplementedException();
        }

        public Task<ApiResponse<int>> UpdateAsync(UserMaster userMaster)
        {
            throw new NotImplementedException();
        }
    }
}