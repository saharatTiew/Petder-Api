namespace petder.Models.ViewModels.Chats
{
    public class MessageViewModel
    {
        public long SessionId { get; set; }
        public string Message { get; set; }
        public long SenderId { get; set; }
        public bool IsSystemMessage { get; set; }
    }
}