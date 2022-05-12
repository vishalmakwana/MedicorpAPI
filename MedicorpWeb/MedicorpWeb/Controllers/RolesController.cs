using Medicorp.Core;
using Medicorp.Core.Entity.Master;
using Medicorp.IServices;
using MedicorpWeb.Models;
using Microsoft.AspNetCore.Mvc;

namespace MedicorpWeb.Controllers
{
    public class RolesController : ApiControllerBase
    {
        private readonly IRolesServicecs _rolesService;

        public RolesController(IRolesServicecs rolesService)
        {
            _rolesService = rolesService;
        }

        [HttpGet]
        [Route("GetRoles")]
        public async Task<IActionResult> Read(string Id)
        {
            AspNetRolesFilter filter = new AspNetRolesFilter() { Id=Id };
            ApiResponse<List<RolesModel>> response = await _rolesService.GetRolesAsync<RolesModel>(filter);
            if (response.Success)
                return Ok(response);
            return BadRequest(response);
        }

    }
}