using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using StudReg.Dtos;
using StudReg.Services.Implementations;
using StudReg.Services.Interfaces;

namespace StudReg.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class GuardianController : ControllerBase
    {
        private readonly IGuardianService _guardianService;

        public GuardianController(IGuardianService guardianService)
        {
            _guardianService = guardianService;
        }

        [HttpPost]
        public async Task<IActionResult> Register([FromBody]CreateGuardianRequestModel model)
        {
            var result = await _guardianService.RegisterGuardian(model);
            if (result.Status)
            {
                return Ok(result);
            }
            return BadRequest(result.Message);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> Get([FromRoute]Guid id)
        {
            var result = await _guardianService.GetGuardian(id);
            if (result.Status)
            {
                return Ok(result);
            }
            return NotFound(result.Message);
        }
    }
}
