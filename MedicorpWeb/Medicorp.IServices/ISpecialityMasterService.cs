using Medicorp.Core;
using Medicorp.Core.Entity.Master;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Medicorp.IServices
{
    public interface ISpecialityMasterService
    {
        Task<ApiResponse<List<T>>> GetSpecialityAsync<T>(SpecilityMasterFilter filter) where T : class;
        Task<ApiResponse<List<SpecilityMaster>>> GetSpecialityAsync(SpecilityMasterFilter filter);
        Task<ApiResponse<int>> UpdateAsync(SpecilityMaster specilityMaster);
        Task<ApiResponse<int>> CreateAsync(SpecilityMaster specilityMaster);
        Task<ApiResponse<int>> DeleteAsync(int SpecialityId);
        Task<bool> ValidateNameAsync(string Title);
        Task<bool> ValidateAsync(int SpecialityId, string Title);
    }
}
