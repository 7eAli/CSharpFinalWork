using MessagesApp.Dto;

namespace MessagesApp.Repo
{
    public interface IMessageRepository
    {
        public string SendMessage(MessageDto messageDto);
        public IEnumerable<MessageDto> ReadAllMessages(Guid receiverId);
        public IEnumerable<MessageDto> ReadUnreadMessages(Guid receiverId);
        public IEnumerable<MessageDto> ReadSentMessages(Guid senderId);
    }
}
