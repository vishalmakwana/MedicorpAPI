using Medicorp.Core;
using Medicorp.Core.Entity;
using Medicorp.IServices;
using MedicorpWeb.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace MedicorpWeb.Controllers
{
    [Authorize]
    public class OrganizationMasterController : ApiControllerBase
    {
       private readonly IOrganizationMaster _organizationMasterService;

        public OrganizationMasterController(IOrganizationMaster organizationMasterService)
        {
            _organizationMasterService = organizationMasterService;
        }

        [HttpGet]
        [Route("GetOrganization")]
        public async Task<IActionResult> Read(int id)
        {
            OrganizationMasterFilter filter = new OrganizationMasterFilter() { OrganizationId = id };
            ApiResponse<List<OrganizationMasterModel>> response = await _organizationMasterService.GetOrganizationAsync<OrganizationMasterModel>(filter);
            if (response.Success)
                return Ok(response);
            return BadRequest(response);
        }

        [HttpPut]
        [Route("UpdateOrganization")]
        public async Task<IActionResult> Update([FromBody] OrganizationMasterModel model)
        {
            if (model == null) throw new ArgumentNullException(nameof(model));
            ApiResponse<int> response = await _organizationMasterService.UpdateAsync(
                new OrganizationMaster()
                {
                    OrganizationId = model.OrganizationId,
                    OrganizationName = model.OrganizationName,
                    IsActive = model.IsActive,
                    UpdateBy = GetUserId(User),
                    UpdateDate = DateTime.UtcNow
                });
            if (response.Success)
                return Ok(response);
            return BadRequest(response);
        }

        [HttpPost]
        [Route("InsertOrganization")]
        public async Task<IActionResult> Create([FromBody] OrganizationMasterModel model)
        {
            if (model == null) throw new ArgumentNullException(nameof(model));
            ApiResponse<int> response = await _organizationMasterService.CreateAsync(
                new OrganizationMaster()
                {
                    OrganizationName = model.OrganizationName,
                    IsActive = model.IsActive,
                    InsertBy = GetUserId(User),
                    InsertedDate = DateTime.UtcNow
                });
            if (response.Success)
                return Ok(response);
            return BadRequest(response);
        }

        [HttpDelete]
        [Route("DeleteOrganization")]
        public async Task<IActionResult> Delete([FromQuery] int id)
        {
            ApiResponse<int> response = await _organizationMasterService.DeleteAsync(id);
            if (response.Success)
                return Ok(response);
            return BadRequest(response);
        }
    }
}