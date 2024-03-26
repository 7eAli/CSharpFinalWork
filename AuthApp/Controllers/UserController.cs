using AuthApp.Client;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace AuthApp.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IUserClient _userClient;
        public UserController(IUserClient userClient)
        {
            _userClient = userClient;
        }

        [HttpDelete]
        [Route(template:"DeleteUser")]
        [Authorize(Roles = "Administrator")]
        public async Task<IActionResult> DeleteUser([FromQuery] string email)
        {
            try
            {
                var res = await _userClient.DeleteUser(email);
                return Ok(res);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }
        

        [HttpGet]
        [Route(template: "GetUsers")]
        [Authorize(Roles = "Administrator")]
        public async Task<IActionResult> GetUsers()
        {
            try
            {
                var res = await _userClient.GetUsers();
                return Ok(res);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }
    }
}
