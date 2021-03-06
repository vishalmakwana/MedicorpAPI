using Medicorp.Core;
using Medicorp.Core.Entity;


namespace Medicorp.IServices
{
    public interface IUserMasterService
    {
        Task<ApiResponse<List<UserMaster>>> GetUserAsync(UserMasterFilter filter);
        Task<ApiResponse<List<T>>> GetUserAsync<T>(UserMasterFilter filter) where T : class;
        Task<ApiResponse<int>> UpdateAsync(UserMaster userMaster);
        Task<ApiResponse<int>> CreateAsync(UserMaster userMaster);
        Task<ApiResponse<int>> DeleteAsync(int userId);
       
    }
}
