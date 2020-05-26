using System.ComponentModel.DataAnnotations;
using System;
using System.ComponentModel.DataAnnotations.Schema;
using System.Collections.Generic;
using Newtonsoft.Json;
namespace petder.Models.DataModels
{
    public class Pet : Entity
    {

        [Key]
        public long pet_id { get; set; }
        public long user_id { get; set; }
        public long breed_id { get; set; }
        [StringLength(200)]
        public string name { get; set; }
        [StringLength(10)]
        public string gender { get; set; }
        public DateTime birth_datetime { get; set; }
        public string description { get; set; }
        public long number_of_like { get; set; }
        public bool is_current { get; set; }
        [JsonIgnore]
        [ForeignKey("breed_id")]
        public virtual Breed Breed { get; set; }
        [JsonIgnore]
        [ForeignKey("user_id")]
        public virtual User User { get; set; }

        // [JsonIgnore]
        [InverseProperty("Pet")]
        public virtual ICollection<PetImage> PetImages { get; set; }
        [JsonIgnore]
        [InverseProperty("BlockerPet")]
        public virtual ICollection<BlockList> BlockerLists { get; set; }
        [JsonIgnore]
        [InverseProperty("BlockedPet")]
        public virtual ICollection<BlockList> BlockedLists { get; set; }
        [JsonIgnore]
        [InverseProperty(nameof(RequestList.RequesterPet))]
        public virtual ICollection<RequestList> RequesterLists { get; set; }
        [JsonIgnore]
        [InverseProperty(nameof(RequestList.RequestedPet))]
        public virtual ICollection<RequestList> RequestedLists { get; set; }
    }
}