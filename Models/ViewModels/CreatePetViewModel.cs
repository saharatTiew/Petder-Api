using System.Collections.Generic;

namespace petder.Models.ViewModels
{
    public class CreatePetViewModel
    {
        public string Name { get; set; }
        public long BreedId { get; set; }
        public string BirthDatetime { get; set; }
        public string Description { get; set; }
        public string Gender { get; set; }
        public List<CreatePetImagesViewModel> PetImages { get; set; }
    }

    public class CreatePetImagesViewModel
    {
        public string ImageUrls { get; set; }
        public bool IsProfile {get; set;}
    }
}