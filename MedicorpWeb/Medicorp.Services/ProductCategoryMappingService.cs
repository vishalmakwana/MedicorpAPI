using Dapper;
using Medicorp.Core;
using Medicorp.Core.CustomException;
using Medicorp.Core.Entity.Master;
using Medicorp.Data.InterFace;
using Medicorp.IServices;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Transactions;

namespace Medicorp.Services
{
    public class ProductCategoryMappingService : IProductCategoryMappingService
    {
        private readonly IDapperHelper _dapperHelper;
        public ProductCategoryMappingService(IDapperHelper dapperHelper)
        {
            _dapperHelper = dapperHelper;
        }
        public async Task<ApiResponse<int>> CreateAsync(ProductCategoryMapping productCategoryMapping)
        {
            ApiResponse<int> response = new ApiResponse<int>() { Success = true };
            using (TransactionScope transactionScope = new TransactionScope(TransactionScopeAsyncFlowOption.Enabled))
            {
                try
                {
                   DynamicParameters dbPara = new DynamicParameters();
                    dbPara.Add("@OrganizationId", productCategoryMapping.OrganizationId, DbType.Int32);
                    dbPara.Add("@CategoryId", productCategoryMapping.CategoryId, DbType.Int32);
                    dbPara.Add("@ProductId", productCategoryMapping.ProductId, DbType.Int32);
                    dbPara.Add("@IsActive", productCategoryMapping.IsActive, DbType.Boolean);
                    dbPara.Add("@InsertBy", productCategoryMapping.InsertdBy, DbType.String);
                    dbPara.Add("@InsertDate", productCategoryMapping.InsertedDate, DbType.DateTime);
                    response.Result = await _dapperHelper.GetAsync<int>(sp: SqlObjectName.ProductCategoryMappingInsert,
                                                  parms: dbPara,
                                                  commandType: CommandType.StoredProcedure);


                    transactionScope.Complete();
                }
                catch (Exception ex)
                {
                    response.ConstructErrorResponse("ProductCategoryMappingService CreateAsync", ex.Message);
                }
            }
            return response;
        }

           }
}
