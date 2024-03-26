using Microsoft.AspNetCore.Mvc;
using System.Text.Json;
using UsersApp.Dto;
using UsersApp.Repo;

namespace UsersApp.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        private IUserRepository _userRepository;
        public UsersController(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        [HttpPost]
        [Route(template: "AddAdmin")]
        public IActionResult AddAdmin(UserDto userDto)
        {
            try
            {
                var res = _userRepository.AddUser(userDto);
                return Ok(res);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }            
        }


        [HttpPost]
        [Route(template: "AddUser")]
        public IActionResult AddUser(UserDto userDto)
        {
            try
            {
                var res = _userRepository.AddUser(userDto);
                return Ok(res);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }            
        }

        [HttpDelete]
        [Route(template: "DeleteUser")]
        public IActionResult DeleteUser(string email)
        {
            try
            {
                var res = _userRepository.DeleteUser(email);
                return Ok(res);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }            
        }

        [HttpGet]
        [Route(template:"GetUserId")]
        public IActionResult GetUserId(string email)
        {
            try
            {
                var userId = _userRepository.GetUserId(email);
                return Ok(userId);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
            
        }
        [HttpGet]
        [Route(template: "GetUserRole")]
        public IActionResult GetUserRole([FromQuery] string email, string password)
        {
            try
            {
                var userRole = _userRepository.GetUserRole(email, password);
                return Ok(userRole);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }
        [HttpGet]
        [Route(template: "GetUsers")]
        public IActionResult GetUsers()
        {
            try
            {
                var users = _userRepository.GetUsers();
                var usersContainer = JsonSerializer.Serialize(users);
                return Ok(usersContainer);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }
    }
}
