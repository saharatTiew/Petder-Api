using System.ComponentModel.DataAnnotations;
using System;
using System.ComponentModel.DataAnnotations.Schema;
using Newtonsoft.Json;

namespace petder.Models.DataModels
{
    public class PetImage : Entity
    {
        [Key]
        public long image_id { get; set; }
        public long pet_id { get; set; }
        public bool is_profile { get; set; }
        [Column(TypeName = "varchar(MAX)")]
        public string image_url { get; set; }
        [JsonIgnore]
        [ForeignKey("pet_id")]
        public virtual Pet Pet { get; set; }
    }
}