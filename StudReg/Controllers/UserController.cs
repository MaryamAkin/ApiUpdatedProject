using System.Security.Claims;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using MySqlX.XDevAPI.Common;
using StudReg.Dtos;
using StudReg.Services.Interfaces;

namespace StudReg.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IUserService _userService;

        public UserController(IUserService userService)
        {
            _userService = userService;
        }
        [HttpPost]
        public async Task<IActionResult> Register(CreateUserRequestModel model)
        {
            var result = await _userService.CreateAsync(model);
            if(result.Status)
            {
                return Ok(result);
            }
            return BadRequest(result.Message);
        }

        [HttpGet]
        public async Task<IActionResult> GetUsers()
        {
            var result = await _userService.GetAllAsync();
            return Ok(result);
        }
        [ApiKey]
        [HttpGet("{id}")]
        public async Task<IActionResult> Get(Guid id)
        {
            var result = await _userService.GetAsync(id);
            if(result.Status)
            {
                return Ok(result);
            }
            return NotFound(result.Message);
        }
        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] LoginRequestModel model)
        {
            var response =  _userService.LoginAsync(model);
            if (response.Status)
            {
                return Ok(JwtTokenGenerator.GenerateJwtToken(response.Data.Id, response.Data.Email, response.Data.Roles));
               
            }
            return BadRequest();
            
        }
    }
}
