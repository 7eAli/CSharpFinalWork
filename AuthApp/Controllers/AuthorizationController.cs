using AuthApp.Client;
using AuthApp.Models.Authorization;
using AuthApp.Models.User;
using AuthApp.rsa;
using AuthApp.Utilities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Reflection;
using System.Security.Claims;

namespace AuthApp.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class AuthorizationController : ControllerBase
    {
        private readonly IConfiguration _configuration;
        private readonly ILoginClient _loginClient;

        public AuthorizationController(IConfiguration configuration, ILoginClient loginClient)
        {
            _configuration = configuration;
            _loginClient = loginClient;
        }

        private string GenerateToken(UserDto user)
        {
            var key = new RsaSecurityKey(KeyProvider.GetPrivateKey());

            var credentials = new SigningCredentials(key, SecurityAlgorithms.RsaSha256Signature);

            var claim = new[]
            {
                new Claim(ClaimTypes.PrimarySid, GetUserId(user.Email)),
                new Claim(ClaimTypes.NameIdentifier, user.Email),
                new Claim(ClaimTypes.Role, RoleConverter.ConvertRole(user.RoleId).ToString()),
            };

            var token = new JwtSecurityToken(
                                            _configuration["Jwt:Issuer"],
                                            _configuration["Jwt:Audience"],
                                            claim,
                                            expires: DateTime.Now.AddMinutes(60),
                                            signingCredentials: credentials);

            return new JwtSecurityTokenHandler().WriteToken(token);
        }

        private string GetUserId(string email)
        {
            return _loginClient.GetUserId(email).Result.ToString();
        }

        [AllowAnonymous]
        [HttpPost]
        [Route(template:"RegisterAdmin")]
        public async Task<IActionResult> RegisterAdmin([FromBody] LoginModel model)
        {
            try
            {
                if (!PasswordCheck.CheckPassword(model.Password)) 
                {
                    return BadRequest("Пароль должен иметь длину 10 и более символов, содержать символы пунктуации, цифры, а также буквы в верхнем и нижнем регистрах");
                }
                if (!EmailCheck.CheckEmail(model.Email))
                {
                    return BadRequest("Неправильный формат email");
                }
                var res = await _loginClient.RegisterAdmin(new UserDto { Email = model.Email, Password = model.Password, RoleId= RoleId.Admin});
                return Ok(res);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }

        [AllowAnonymous]
        [HttpPost]
        [Route(template:"RegisterUser")]
        public async Task<IActionResult> RegisterUser([FromBody] LoginModel model)
        {
            try
            {
                var res = await _loginClient.RegisterUser(new UserDto { Email = model.Email, Password = model.Password, RoleId = RoleId.User });
                return Ok(res);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }

        [AllowAnonymous]
        [HttpGet]
        [Route(template:"Login")]
        public async Task<IActionResult> Login([FromQuery] LoginModel model)
        {
            try
            {
                var roleType = await _loginClient.GetUserRole(model);                

                var userModel = new UserDto { Email = model.Email, Password = model.Password, RoleId = RoleConverter.ConvertRole(roleType) };

                var token = GenerateToken(userModel);

                return Ok(token);
            }catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }

        }

        
    }
}
