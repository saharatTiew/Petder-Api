using System.Collections.Generic;

namespace petder.Models.ViewModels.Requests
{
    public class PetRequestViewModel
    {
        public long PetId { get; set; }
        public string Name { get; set; }
        public string Breed { get; set; }
        public string Address { get; set; }
        public List<string> ImageUrls { get; set; }
    }
}