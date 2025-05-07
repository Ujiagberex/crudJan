using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using WebApiClass.IServices;

namespace WebApiClass.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class VerifyNumberController : ControllerBase
    {
        private readonly INumberCheckService checkService;

        public VerifyNumberController(INumberCheckService checkService)
        {
            this.checkService = checkService;
        }

        [HttpGet("GetNumberDetails")]
        public async Task<IActionResult> GetNumberDetails([FromQuery] string number)
        {
            var response = await checkService.GetCountryNumber(number);
            return Ok(response);
        }
    }
}
