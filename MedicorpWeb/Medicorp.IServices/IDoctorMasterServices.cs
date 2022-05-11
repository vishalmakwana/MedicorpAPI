using Medicorp.Core;
using Medicorp.Core.Entity.Master;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Medicorp.IServices
{
    public interface IDoctorMasterServices
    {
        Task<ApiResponse<List<DoctorMaster>>> GetDoctorAsync(DoctorMasterFilter filter);
        Task<ApiResponse<int>> UpdateAsync(DoctorMaster doctorMaster);
        Task<ApiResponse<int>> CreateAsync(DoctorMaster doctorMaster);
        Task<ApiResponse<int>> DeleteAsync(int DoctorId);
        Task<ApiResponse<List<T>>> GetDoctorAsync<T>(DoctorMasterFilter filter) where T : class;
    }
}
