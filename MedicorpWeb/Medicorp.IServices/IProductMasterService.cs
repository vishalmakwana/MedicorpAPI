using Medicorp.Core;
using Medicorp.Core.Entity.Master;

namespace Medicorp.IServices
{
    public interface IProductMasterService
    {
        Task<ApiResponse<List<ProductMaster>>> GetProductAsync(ProductMasterFilter filter);
        Task<ApiResponse<int>> UpdateAsync(ProductMaster productMaster);
        Task<ApiResponse<int>> CreateAsync(ProductMaster productMaster);
        Task<ApiResponse<int>> DeleteAsync(int ProductId);
        Task<ApiResponse<List<T>>> GetProductAsync<T>(ProductMasterFilter filter) where T : class;

    }
}
