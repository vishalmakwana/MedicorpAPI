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
    }
}