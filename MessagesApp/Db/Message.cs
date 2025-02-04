﻿namespace MessagesApp.Db
{
    public class Message
    {
        public Guid Id { get; set; }
        public string Content { get; set; }
        public bool IsRead { get; set; } = false;
        public Guid SenderId { get; set; }
        public Guid ReceiverId { get; set; }
    }
}
