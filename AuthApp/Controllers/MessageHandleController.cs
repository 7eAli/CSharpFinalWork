using AuthApp.Client;
using AuthApp.Models.Message;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Reflection;
using System.Security.Claims;

namespace AuthApp.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class MessageHandleController : ControllerBase
    {
        private readonly IMessageClient _messageClient;
        public MessageHandleController(IMessageClient messageClient)
        {
            _messageClient = messageClient;
        }

        [HttpPost]
        [Route(template:"SendMessage")]
        [Authorize(Roles = "Administrator, User")]
        public async Task<IActionResult> SendMessage([FromBody] string content, string receiverId) 
        {
            try
            {
                var msgModel = new MessageModel { Content = content, ReceiverId = Guid.Parse(receiverId), SenderId = Guid.Parse(GetCurrentUser()) };
                var message = await _messageClient.SendMessage(msgModel);
                return Ok(message);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }

        [HttpGet]
        [Route(template:"GetUnreadReceivedMessages")]
        public async Task<IActionResult> GetUnreadReceivedMessages() 
        {
            try
            {
                var receiverId = Guid.Parse(GetCurrentUser());
                var messages = await _messageClient.GetUnreadReceivedMessages(receiverId);
                return Ok(messages);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }

        [HttpGet]
        [Route(template: "GetAllReceivedMessages")]
        public async Task<IActionResult> GetAllReceivedMessages()
        {
            try
            {
                var receiverId = Guid.Parse(GetCurrentUser());
                var messages = await _messageClient.GetAllReceivedMessages(receiverId);
                return Ok(messages);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }

        [HttpGet]
        [Route(template: "GetSentMessages")]
        public async Task<IActionResult> GetSentMessages()
        {
            try
            {
                var senderId = Guid.Parse(GetCurrentUser());
                var messages = await _messageClient.GetSentMessages(senderId);
                return Ok(messages);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }


            
        private string GetCurrentUser()
        {
            var userIdentity = HttpContext.User.Identity as ClaimsIdentity;
            IEnumerable<Claim> claims = userIdentity.Claims;
            var currentUserId = claims.FirstOrDefault(t => t.Type == ClaimTypes.PrimarySid).Value;
            return currentUserId;
        }        
    }
}
