using Microsoft.AspNetCore.Mvc;
using StudReg.Dtos;
using StudReg.Services.Implementations;
using StudReg.Services.Interfaces;

namespace StudReg.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class StudentController : ControllerBase
    {
        private readonly IStudentService _studentService;
        public StudentController(IStudentService studentService)
        {
            _studentService = studentService;
        }

        [HttpPost]
        public async Task<IActionResult> Register([FromBody]CreateStudentRequestModel model)
        {
            var result = await _studentService.CreateAsync(model);
            if (result.Status)
            {
                return Ok(result);
            }
            return BadRequest(result.Message);
        }
        [HttpGet]
        public async Task<IActionResult> GetStudents()
        {
            var result = await _studentService.GetAllAsync();
            return Ok(result);
        }
        [HttpGet("id")]
        public async Task<IActionResult> Get([FromQuery]Guid id)
        {
            var result = await _studentService.GetAsync(id);
            if (result.Status)
            {
                return Ok(result);
            }
            return NotFound(result.Message);
        }
    }
}
