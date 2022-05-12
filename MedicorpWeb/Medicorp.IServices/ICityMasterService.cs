using Medicorp.Core;
using Medicorp.Core.Entity.Master;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Medicorp.IServices
{
    public interface ICityMasterService
    {
        Task<ApiResponse<List<CityMaster>>> GetCityAsync(CityMaster cityMaster);
        Task<ApiResponse<List<T>>> GetCityAsync<T>(CityMaster cityMaster) where T : class;
        Task<ApiResponse<int>> CreateAsync(CityMaster cityMaster);
    }
}
