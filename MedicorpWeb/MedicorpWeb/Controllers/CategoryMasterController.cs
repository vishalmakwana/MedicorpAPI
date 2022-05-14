using Medicorp.Core;
using Medicorp.Core.Entity.Master;
using Medicorp.IServices;
using MedicorpWeb.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace MedicorpWeb.Controllers
{
    public class CategoryMasterController : ApiControllerBase
    {
        private readonly ICategoryMasterService _categoryMasterService;

        public CategoryMasterController(ICategoryMasterService categoryMasterService)
        {
            _categoryMasterService = categoryMasterService;
        }

        [AllowAnonymous]
        [HttpGet]
        [Route("GetCategory")]
        public async Task<IActionResult> Read(int id)
        {
            CategoryMasterFilter filter = new CategoryMasterFilter() { CategoryId = id };
            ApiResponse<List<CategoryMasterModel>> response = await _categoryMasterService.GetCategoryAsync<CategoryMasterModel>(filter);
            if (response.Success)
                return Ok(response);
            return BadRequest(response);
        }

        [AllowAnonymous]
        [HttpPut]
        [Route("UpdateCategory")]
        public async Task<IActionResult> Update([FromBody] CategoryMasterModel model)
        {
            if (model == null) throw new ArgumentNullException(nameof(model));
            ApiResponse<int> response = await _categoryMasterService.UpdateAsync(
                new CategoryMaster()
                {
                    OrganizationId = model.OrganizationId,
                   CategoryName = model.CategoryName,
                    IsActive = model.IsActive,
                    UpdatedBy = GetUserId(User),
                    UpdateDate = DateTime.UtcNow
                });
            if (response.Success)
                return Ok(response);
            return BadRequest(response);
        }
        [AllowAnonymous]
        [HttpPost]
        [Route("CreateCategory")]
        public async Task<IActionResult> Create([FromBody] CategoryMasterModel model)
        {
            if (model == null) throw new ArgumentNullException(nameof(model));
            ApiResponse<int> response = await _categoryMasterService.CreateAsync(
                new CategoryMaster()
                {
                    OrganizationId = model.OrganizationId,
                    CategoryName = model.CategoryName,
                    IsActive = model.IsActive,
                    InsertdBy = GetUserId(User),
                    InsertedDate = DateTime.UtcNow
                });
            if (response.Success)
                return Ok(response);
            return BadRequest(response);
        }
        [AllowAnonymous]
        [HttpDelete]
        [Route("DeleteCategory")]
        public async Task<IActionResult> Delete([FromQuery] int id)
        {
            ApiResponse<int> response = await _categoryMasterService.DeleteAsync(id);
            if (response.Success)
                return Ok(response);
            return BadRequest(response);
        }
    }
}