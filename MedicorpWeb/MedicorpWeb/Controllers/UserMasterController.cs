using Medicorp.Core;
using Medicorp.Core.Entity;
using Medicorp.IServices;
using MedicorpWeb.Models;
using Microsoft.AspNetCore.Mvc;

namespace MedicorpWeb.Controllers
{
    public class UserMasterController : ApiControllerBase
    {
        private readonly IUserMasterService _userMasterService;

    public UserMasterController(IUserMasterService userMasterService)
    {
            _userMasterService = userMasterService;
    }

    [HttpGet]
    [Route("ReadUserMaster")]
    public async Task<IActionResult> Read(int id)
    {
            UserMasterFilter filter = new UserMasterFilter() { UserId = id };
        ApiResponse<List<UserMasterModel>> response = await _userMasterService.GetUserAsync<UserMasterModel>(filter);
        if (response.Success)
            return Ok(response);
        return BadRequest(response);
    }

    [HttpPut]
    [Route("UpdateUserMaster")]
    public async Task<IActionResult> Update([FromBody] UserMasterModel model)
    {
        if (model == null) throw new ArgumentNullException(nameof(model));
        ApiResponse<int> response = await _userMasterService.UpdateAsync(
            new UserMaster()
            {
                OrganizationId = model.OrganizationId,
                Username = model.Username,
                Firstname = model.Firstname,
                Lastname = model.Lastname,
                Email = model.Email,
                EmailConfirmed = model.EmailConfirmed,
                PhoneNumber = model.PhoneNumber,
                PhoneNumberConfirmed = model.PhoneNumberConfirmed,
                IsActive = model.IsActive
             });
        if (response.Success)
            return Ok(response);
        return BadRequest(response);
    }

    [HttpPost]
    [Route("CreateUserMaster")]
    public async Task<IActionResult> Create([FromBody] UserMasterModel model)
    {
        if (model == null) throw new ArgumentNullException(nameof(model));
        ApiResponse<int> response = await _userMasterService.CreateAsync(
            new UserMaster()
            {
                OrganizationId = model.OrganizationId,
                Username = model.Username,
                Firstname = model.Firstname,
                Lastname = model.Lastname,
                Email = model.Email,
                EmailConfirmed = model.EmailConfirmed,
                PhoneNumber = model.PhoneNumber,
                PhoneNumberConfirmed = model.PhoneNumberConfirmed,
                IsActive = model.IsActive
            });
        if (response.Success)
            return Ok(response);
        return BadRequest(response);
    }

    [HttpDelete]
    [Route("DeleteUserMaster")]
    public async Task<IActionResult> Delete([FromQuery] int id)
    {
        ApiResponse<int> response = await _userMasterService.DeleteAsync(id);
        if (response.Success)
            return Ok(response);
        return BadRequest(response);
    }
}
}