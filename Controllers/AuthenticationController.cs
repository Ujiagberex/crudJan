using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using WebApiClass.DTO;
using WebApiClass.IServices;

namespace WebApiClass.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthenticationController : ControllerBase
    {
        private readonly IAuth _auth;

        public AuthenticationController(IAuth auth)
        {
            _auth = auth;
        }

        [HttpPost("CreateNewUser")]
        public async Task<IActionResult> CreateNewUser([FromBody] RegisterDTO registerDTO)
        {
            var response = await _auth.CreateUser(registerDTO);
            return Ok(response);
        }

        [HttpPost("LoginUser")]
        public async Task<IActionResult> LogIn([FromBody]LogInDTO logInDTO)
        {
            var response = await _auth.LogInUser(logInDTO);
            return Ok(response);
        }



    }
}
