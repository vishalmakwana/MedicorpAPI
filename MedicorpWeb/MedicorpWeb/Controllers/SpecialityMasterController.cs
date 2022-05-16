using Medicorp.Core;
using Medicorp.Core.Entity.Master;
using Medicorp.IServices;
using MedicorpWeb.Models;
using Microsoft.AspNetCore.Mvc;

namespace MedicorpWeb.Controllers
{
    public class SpecialityMasterController : ApiControllerBase
    {
        private readonly ISpecialityMasterService _specialityMasterService;

        public SpecialityMasterController(ISpecialityMasterService specialityMasterService)
        {
            _specialityMasterService = specialityMasterService;
        }
        [HttpPost]
        [Route("CreateSpeciality")]
        public async Task<IActionResult> Create([FromBody] SpecialityMasterModel model)
        {
            if (model == null) throw new ArgumentNullException(nameof(model));
            ApiResponse<int> response = await _specialityMasterService.CreateAsync(
                new SpecilityMaster()
                {
                    OrganizationId = model.OrganizationId,
                    Title = model.Title,
                    Description = model.Description,
                    IsActive = model.IsActive,
<<<<<<< HEAD
                    InsertdBy = GetUserId(User),
=======
                    InsertdBy = GetUserId(User),    
>>>>>>> efbde2b0d63a6da6132242e2ed57eea04d9abe47
                    InsertedDate = DateTime.UtcNow
                });


            if (response.Success)
                return Ok(response);
            return BadRequest(response);
        }
        [HttpGet]
        [Route("GetSpeciality")]
        public async Task<IActionResult> Read(int id)
        {
<<<<<<< HEAD
            SpecilityMasterFilter filter = new SpecilityMasterFilter() { SpecialityId = id };
=======
            SpecilityMasterFilter filter = new SpecilityMasterFilter() { SpecilityId = id };
>>>>>>> efbde2b0d63a6da6132242e2ed57eea04d9abe47
            ApiResponse<List<SpecialityMasterModel>> response = await _specialityMasterService.GetSpecialityAsync<SpecialityMasterModel>(filter);
            if (response.Success)
                return Ok(response);
            return BadRequest(response);
        }

        [HttpPut]
        [Route("UpdateSpeciality")]
        public async Task<IActionResult> Update([FromBody] SpecialityMasterModel model)
        {
            if (model == null) throw new ArgumentNullException(nameof(model));
            ApiResponse<int> response = await _specialityMasterService.UpdateAsync(
                new SpecilityMaster()
                {
<<<<<<< HEAD
                    SpecialityId = model.SpecialityId,
=======
                    SpecilityId = model.SpecilityId,
>>>>>>> efbde2b0d63a6da6132242e2ed57eea04d9abe47
                    OrganizationId = model.OrganizationId,
                    Title = model.Title,
                    Description = model.Description,
                    IsActive = model.IsActive,
                    UpdatedBy = GetUserId(User),
                    UpdateDate = DateTime.UtcNow
                });
            if (response.Success)
                return Ok(response);
            return BadRequest(response);
        }
        [HttpDelete]
        [Route("DeleteSpeciality")]
        public async Task<IActionResult> Delete([FromQuery] int id)
        {
            ApiResponse<int> response = await _specialityMasterService.DeleteAsync(id);
            if (response.Success)
                return Ok(response);
            return BadRequest(response);
        }
    }
}
