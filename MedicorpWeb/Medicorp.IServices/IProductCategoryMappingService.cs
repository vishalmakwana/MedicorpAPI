using Medicorp.Core;
using Medicorp.Core.Entity.Master;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Medicorp.IServices
{
    public interface IProductCategoryMappingService
    {
        Task<ApiResponse<int>> CreateAsync(ProductCategoryMapping productCategoryMapping);
    }
}
