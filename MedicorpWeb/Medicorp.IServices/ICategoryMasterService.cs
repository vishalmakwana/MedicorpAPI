using Medicorp.Core;
using Medicorp.Core.Entity.Master;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Medicorp.IServices
{
    public interface ICategoryMasterService
    {
        Task<ApiResponse<List<CategoryMaster>>> GetCategoryAsync(CategoryMasterFilter filter);
        Task<ApiResponse<List<T>>> GetCategoryAsync<T>(CategoryMasterFilter filter) where T : class;
        Task<ApiResponse<int>> UpdateAsync(CategoryMaster categoryMaster);
        Task<ApiResponse<int>> CreateAsync(CategoryMaster categoryMaster);
        Task<ApiResponse<int>> DeleteAsync(int categoryId);
        Task<ApiResponse<int>> GetAsync(int id);
        Task<bool> ValidateNameAsync(string CategoryName);
        Task<bool> ValidateAsync(int CategoryId, string CategoryName);
    }
}
