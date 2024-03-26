namespace AuthApp.Models.Message
{
    public class MessageModel
    {
        public string Content { get; set; }
        public Guid SenderId { get; set; }
        public Guid ReceiverId { get; set; }
    }
}
