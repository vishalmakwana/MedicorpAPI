using Medicorp.Core;
using Medicorp.Core.Entity.Master;
using Medicorp.IServices;
using MedicorpWeb.Models;
using Microsoft.AspNetCore.Mvc;

namespace MedicorpWeb.Controllers
{
    public class ProductMasterController : ApiControllerBase
    {
        private readonly IProductMasterService _productMasterService;

        public ProductMasterController(IProductMasterService productMasterService)
        {
            _productMasterService = productMasterService;
        }

        [HttpGet]
        [Route("GetProduct")]
        public async Task<IActionResult> Read(int id)
        {
            ProductMasterFilter filter = new ProductMasterFilter() { ProductId = id };
            ApiResponse<List<ProductMasterModel>> response = await _productMasterService.GetProductAsync<ProductMasterModel>(filter);
            if (response.Success)
                return Ok(response);
            return BadRequest(response);
        }

        [HttpPut]
        [Route("UpdateProduct")]
        public async Task<IActionResult> Update([FromBody] ProductMasterModel model)
        {
            if (model == null) throw new ArgumentNullException(nameof(model));
            ApiResponse<int> response = await _productMasterService.UpdateAsync(
                new ProductMaster()
                {
                    OrganizationId = model.OrganizationId,
                    ProductName = model.ProductName,
                    ShortDescription = model.ShortDescription,
                    LongDescription = model.LongDescription,
                    MRP = model.MRP,
                    IsActive = model.IsActive,
                    UpdatedBy = GetUserId(User),
                    UpdateDate = DateTime.UtcNow,
                     CategoryId = model.CategoryId
                });
            if (response.Success)
                return Ok(response);
            return BadRequest(response);
        }

        [HttpPost]
        [Route("CreateProduct")]
        public async Task<IActionResult> Create([FromBody] ProductMasterModel model)
        {
            if (model == null) throw new ArgumentNullException(nameof(model));
            ApiResponse<int> response = await _productMasterService.CreateAsync(
                new ProductMaster()
                {
                    ProductName = model.ProductName,
                    ShortDescription = model.ShortDescription,
                    LongDescription = model.LongDescription,
                    MRP = model.MRP,
                    OrganizationId= model.OrganizationId,
                    IsActive = model.IsActive,
                    InsertdBy = GetUserId(User),
                    InsertedDate = DateTime.UtcNow,
                    CategoryId=model.CategoryId

                });
            if (response.Success)
                return Ok(response);
            return BadRequest(response);
        }

        [HttpDelete]
        [Route("DeleteProduct")]
        public async Task<IActionResult> Delete([FromQuery] int id)
        {
            ApiResponse<int> response = await _productMasterService.DeleteAsync(id);
            if (response.Success)
                return Ok(response);
            return BadRequest(response);
        }
    }
}