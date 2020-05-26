namespace petder.Models.ViewModels.Requests
{
    public class PendingRequestViewModel
    {
        public long RequestListId { get; set; }
        public long RequesterPetId { get; set; }
        public string ImageUrl { get; set; }
        public string Breed { get; set; }
        public string Name { get; set; }
    }
}