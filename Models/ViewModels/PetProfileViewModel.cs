namespace petder.Models.ViewModels
{
    public class PetProfileViewModel
    {
        public long PetId { get; set; }
        public long ImageId { get; set; }
        public string Name { get; set; }
        public string Breed { get; set; }
        public string Address { get; set; }
        public string ImageUrl { get; set; }
    }
}