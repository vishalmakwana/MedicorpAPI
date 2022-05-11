using Medicorp.Core;
using Medicorp.Core.Entity.Master;
using Medicorp.IServices;
using MedicorpWeb.Models;
using Microsoft.AspNetCore.Mvc;

namespace MedicorpWeb.Controllers
{
    public class CityMasterController : ApiControllerBase
    {
        private readonly ICityMasterService _cityMasterService;

        public CityMasterController(ICityMasterService cityMasterService)
        {
            _cityMasterService = cityMasterService;
        }

        [HttpGet]
        [Route("getCity")]
        public async Task<IActionResult> Read(int id)
        {
            CityMaster cityMaster = new CityMaster() { CityId = id };
            ApiResponse<List<CityMasterModel>> response = await _cityMasterService.GetCityAsync<CityMasterModel>(cityMaster);
            if (response.Success)
                return Ok(response);
            return BadRequest(response);
        }
        [HttpPost]
        [Route("createCity")]
        public async Task<IActionResult> Create([FromBody] CityMasterModel model)
        {
            if (model == null) throw new ArgumentNullException(nameof(model));
            ApiResponse<int> response = await _cityMasterService.CreateAsync(
                new CityMaster()
                {
                    CityName = model.CityName,
                    StateId = model.StateId,
                    IsActive = model.IsActive
                });
            if (response.Success)
                return Ok(response);
            return BadRequest(response);
        }
    }
}
