using System.Security.Claims;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
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

        [HttpGet("{id}")]
        public async Task<IActionResult> Get([FromQuery]Guid id)
        {
            var result = await _userService.GetAsync(id);
            if(result.Status)
            {
                return Ok(result);
            }
            return NotFound(result.Message);
        }
        [HttpPost]
        public async Task<IActionResult> Login([FromBody] LoginRequestModel model)
        {
            var response = await _userService.LoginAsync(model);
            if (response.Status)
            {
                var claims = new List<Claim>
                {
                    new Claim(ClaimTypes.NameIdentifier,response.Data.Id.ToString()),
                    new Claim(ClaimTypes.Email,response.Data.Email)

                };
                var identity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);
                var principal = new ClaimsPrincipal(identity);
                var properties = new AuthenticationProperties();
                await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, principal, properties);
                return Ok();
            }
            return BadRequest();
            
        }
    }
}
