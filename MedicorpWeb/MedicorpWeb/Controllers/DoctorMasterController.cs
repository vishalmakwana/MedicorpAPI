using Medicorp.Core;
using Medicorp.Core.Entity.Master;
using Medicorp.IServices;
using MedicorpWeb.Models;
using Microsoft.AspNetCore.Mvc;

namespace MedicorpWeb.Controllers
{
    public class DoctorMasterController : ApiControllerBase
    {
        private readonly IDoctorMasterServices _doctorMasterServices;

        public DoctorMasterController(IDoctorMasterServices doctorMasterServices)
        {
            _doctorMasterServices = doctorMasterServices;
        }
        [HttpPost]
        [Route("CreateDoctor")]
        public async Task<IActionResult> Create([FromBody] DoctorMasterModel model)
        {
            if (model == null) throw new ArgumentNullException(nameof(model));
            ApiResponse<int> response = await _doctorMasterServices.CreateAsync(
                new DoctorMaster()
                {
                    FirstName = model.FirstName,
                    LastName = model.LastName,
                    Gender = model.Gender,
                    Email = model.Email,
                    Address = model.Address,
                    CityId = model.CityId,
                    StateId = model.StateId,
                    Mobilenumber = model.Mobilenumber,
                    OrganizationId = model.OrganizationId,
                    IsActive = model.IsActive,
                    InsertdBy = GetUserId(User),
                    InsertedDate = DateTime.UtcNow
                });
            if (response.Success)
                return Ok(response);
            return BadRequest(response);
        }

        [HttpGet]
        [Route("GetDoctor")]
        public async Task<IActionResult> Read(int id)
        {
            DoctorMasterFilter filter = new DoctorMasterFilter() { DoctorId = id };
            ApiResponse<List<DoctorMasterModel>> response = await _doctorMasterServices.GetDoctorAsync<DoctorMasterModel>(filter);
            if (response.Success)
                return Ok(response);
            return BadRequest(response);
        }

        [HttpPut]
        [Route("UpdateDoctor")]
        public async Task<IActionResult> Update([FromBody] DoctorMasterModel model)
        {
            if (model == null) throw new ArgumentNullException(nameof(model));
            ApiResponse<int> response = await _doctorMasterServices.UpdateAsync(
                new DoctorMaster()
                {
                    DoctorId = model.DoctorId,
                    FirstName = model.FirstName,
                    LastName = model.LastName,
                    Gender = model.Gender,
                    Email = model.Email,
                    Address = model.Address,
                    CityId = model.CityId,
                    StateId = model.StateId,
                    Mobilenumber = model.Mobilenumber,
                    OrganizationId = model.OrganizationId,
                    IsActive = model.IsActive,
                    UpdatedBy = GetUserId(User),
                    UpdateDate = DateTime.UtcNow
                });
            if (response.Success)
                return Ok(response);
            return BadRequest(response);
        }
        [HttpDelete]
        [Route("DeleteDoctor")]
        public async Task<IActionResult> Delete([FromQuery] int id)
        {
            ApiResponse<int> response = await _doctorMasterServices.DeleteAsync(id);
            if (response.Success)
                return Ok(response);
            return BadRequest(response);
        }
    }
}
