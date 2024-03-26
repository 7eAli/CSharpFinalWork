using MessagesApp.Dto;
using MessagesApp.Repo;
using Microsoft.AspNetCore.Mvc;
using System.Text.Json;

namespace MessagesApp.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class MessageController : ControllerBase
    {
        private readonly IMessageRepository _messageRepository;

        public MessageController(IMessageRepository messageRepository)
        {
            _messageRepository = messageRepository;
        }

        [HttpPost]
        [Route(template:"SendMessage")]
        public IActionResult SendMessage(MessageDto messageDto) 
        { 
            try
            {
                var res = _messageRepository.SendMessage(messageDto);
                return Ok(res);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
            
        }

        [HttpGet]
        [Route(template:"ReceiveUnreadMessages")]
        public IActionResult ReceiveUnreadMessages([FromQuery] string receiverId)
        {
            try
            {
                
                var messages = _messageRepository.ReadUnreadMessages(Guid.Parse(receiverId));
                var messagesContainer = JsonSerializer.Serialize(messages);
                return Ok(messagesContainer);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }

        [HttpGet]
        [Route(template: "ReceiveAllMessages")]
        public IActionResult ReceiveAllMessages([FromQuery] string receiverId)
        {
            try
            {
                var messages = _messageRepository.ReadAllMessages(Guid.Parse(receiverId));
                var messagesContainer = JsonSerializer.Serialize(messages);
                return Ok(messagesContainer);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }

        [HttpGet]
        [Route(template: "ReceiveSentMessages")]
        public IActionResult ReceiveSentMessages([FromQuery] string senderId)
        {
            try
            {
                var messages = _messageRepository.ReadSentMessages(Guid.Parse(senderId));
                var messagesContainer = JsonSerializer.Serialize(messages);
                return Ok(messagesContainer);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }
    }
}
