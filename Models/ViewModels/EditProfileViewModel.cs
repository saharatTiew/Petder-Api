using System;
using System.Collections.Generic;
using petder.Models.DataModels;

namespace petder.Models.ViewModels
{
    public class EditProfileViewModel
    {
        public long UserId { get; set; }
        public long PetId { get; set; }
        public long BreedId { get; set; }
        public string Name { get; set; }
        public string Gender { get; set; }
        public DateTime BirthDateTime { get; set; }
        public string Description { get; set; }
        public bool IsCurrent { get; set; }
        public string Address { get; set; }
        public long AddressId { get; set; }
        public virtual List<EditImageViewModel> PetImages { get; set; }
    }
}
