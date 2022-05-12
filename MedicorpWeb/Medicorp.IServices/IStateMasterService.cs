using Medicorp.Core;
using Medicorp.Core.Entity.Master;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Medicorp.IServices
{
    public interface IStateMasterService
    {
        Task<ApiResponse<List<StateMaster>>> GetStateAsync(StateMaster stateMaster);
        Task<ApiResponse<List<T>>> GetStateAsync<T>(StateMaster stateMaster) where T : class;
        Task<ApiResponse<int>> CreateAsync(StateMaster stateMaster);
    }
}
