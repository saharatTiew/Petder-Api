using System;

namespace petder.Models.ViewModels.Chats
{
    public class ChatViewModel
    {
        public string SenderName { get; set; }
        public string SenderImageUrl { get; set; }
        public long SenderId { get; set; }
        public DateTime SendDateTime { get; set; }
        public string Message { get; set; }
        public bool IsUnsent { get; set; }
        public bool IsSystemMessage { get; set; }
        public long MessageId { get; set; }

    }
}