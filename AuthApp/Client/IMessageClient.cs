using AuthApp.Models.Message;

namespace AuthApp.Client
{
    public interface IMessageClient
    {
        public Task<Guid> SendMessage(MessageModel message);
        public Task<IEnumerable<MessageModel>> GetAllReceivedMessages(Guid receiverId);
        public Task<IEnumerable<MessageModel>> GetUnreadReceivedMessages(Guid receiverId);
        public Task<IEnumerable<MessageModel>> GetSentMessages(Guid senderId);        
    }
}
