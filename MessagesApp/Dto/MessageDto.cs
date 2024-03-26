namespace MessagesApp.Dto
{
    public class MessageDto
    {        
        public string Content { get; set; }
        public Guid SenderId { get; set; }
        public Guid ReceiverId { get; set; }
    }
}
