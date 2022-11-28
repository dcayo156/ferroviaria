using LaJuana.Application.Contracts.Identity;
using LaJuana.Application.Models;
using LaJuana.Application.Models.Identity;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace LaJuana.API.Controllers
{
    [ApiController]
    [Route("v1/[controller]")]
    public class AccountController : ControllerBase
    {
        private readonly IAuthService _authService;
        private readonly RoleManager<IdentityRole> _roleManager;

        public AccountController(IAuthService authService, RoleManager<IdentityRole> roleManager)
        {
            _authService = authService;
            _roleManager = roleManager;
        }

        [HttpPost("Login")]
        public async Task<ActionResult<AuthResponse>> Login([FromBody] AuthRequest request)
        {
            return Ok(await _authService.Login(request));
        }

        [HttpPost("Register")]
        public async Task<ActionResult<RegistrationResponse>> Register([FromBody] RegistrationRequest request)
        {
            return Ok(await _authService.Register(request));
        }
        [HttpPost("UpdatePassword")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [Authorize]
        public async Task<ActionResult> UpdatePassword([FromBody] UpdatePasswordRequest request)
        {
            await _authService.UpdatePassword(request);
            return NoContent();
        }
        [HttpPost("RefreshToken")]
        public async Task<ActionResult<AuthResponse>> RefreshToken([FromBody] TokenApiModel request)
        {
            try
            {
                var result = await _authService.Refresh(request);
                return Ok(result);
            }
            catch (Exception)
            {
                return Unauthorized();
            }
        }
        [HttpPost("Revoke")]
        public async Task<ActionResult> RevokeToken()
        {
            await _authService.Revoke(User.Identity!.Name);
            return Ok();
        }
        [HttpPut("UpdateRegister")]
        [Authorize]
        public async Task<ActionResult<RegistrationResponse>> UpdateRegister([FromBody] UpdateRegistrationRequest request)
        {
            return Ok(await _authService.UpdateRegister(request));
        }

        [HttpGet("GetUserById/{id}", Name = "GetUserById/{id}")]
        [Authorize]
        [ProducesResponseType(typeof(User), (int)HttpStatusCode.OK)]
        public async Task<ActionResult<User>> GetUserById(string id)
        {
            var user = await _authService.GetUserById(id);
            return Ok(user);
        }


        [HttpGet("GetUsers", Name = "GetUsers")] 
        //[Authorize(Roles = "ADMIN")]
        [ProducesResponseType(typeof(IEnumerable<User>), (int)HttpStatusCode.OK)]
        public async Task<ActionResult<IEnumerable<User>>> GetUsers()
        {
            var users = await _authService.GetUsers();
            return Ok(users);
        }


        [Authorize(Roles = "ADMIN")]
        [HttpPut("role/UpdateRoleAdmin")]
        public async Task<ActionResult<User>> UpdateRoleAdmin(UpdateRoleAdminRequest request)
        {
            var user = await _authService.UpdateRoleAdmin(request.Id, request.Status);
            return Ok(user);
        }


    }
}
