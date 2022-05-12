using Medicorp.Core;
using Medicorp.Core.Entity.Master;
using Medicorp.IServices;
using MedicorpWeb.Models;
using Microsoft.AspNetCore.Mvc;

namespace MedicorpWeb.Controllers
{
    public class StateMasterController : ApiControllerBase
    {
        private readonly IStateMasterService _stateMasterService;

        public StateMasterController(IStateMasterService stateMasterService)
        {
            _stateMasterService = stateMasterService;
        }

        [HttpGet]
        [Route("getState")]
        public async Task<IActionResult> Read(int id)
        {
            StateMaster stateMaster = new StateMaster() { StateId = id };
            ApiResponse<List<StateMasterModel>> response = await _stateMasterService.GetStateAsync<StateMasterModel>(stateMaster);
            if (response.Success)
                return Ok(response);
            return BadRequest(response);
        }
        [HttpPost]
        [Route("createState")]
        public async Task<IActionResult> Create([FromBody] StateMasterModel model)
        {
            if (model == null) throw new ArgumentNullException(nameof(model));
            ApiResponse<int> response = await _stateMasterService.CreateAsync(
                new StateMaster()
                {
                    StateName = model.StateName,
                    IsActive = model.IsActive
                });
            if (response.Success)
                return Ok(response);
            return BadRequest(response);
        }
    }
}
