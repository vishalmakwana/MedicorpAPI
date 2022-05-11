using Medicorp.Core;
using Medicorp.Core.Entity.Master;
using Medicorp.IServices;
using MedicorpWeb.Models;
using Microsoft.AspNetCore.Mvc;

namespace MedicorpWeb.Controllers
{
    public class UserRolesController : ApiControllerBase
    {
        private readonly IUserRolesService _userRolesService;

        public UserRolesController(IUserRolesService userRolesService)
        {
            _userRolesService = userRolesService;
        }

        [HttpGet]
        [Route("GetUserRoles")]
        public async Task<IActionResult> Read(string UserId,string RoleID)
        {
            UserRolesFilter filter = new UserRolesFilter() { UserId = UserId , RoleId=RoleID };
            ApiResponse<List<UserRolesModel>> response = await _userRolesService.GetUserRolesAsync<UserRolesModel>(filter);
            if (response.Success)
                return Ok(response);
            return BadRequest(response);
        }

    }
}