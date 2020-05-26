using System;

namespace petder.Models.ViewModels.Chats
{
    public class SessionViewModel
    {
        public long SessionId { get; set; }
        public long ReceiverPetId { get; set; }
        public string Name { get; set; }
        public string ImageUrl { get; set; }
        public string Message { get; set; }
        public DateTime SentDateTime { get; set; }
    }
}