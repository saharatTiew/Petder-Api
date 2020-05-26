using System.ComponentModel.DataAnnotations;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using Newtonsoft.Json;

namespace petder.Models.DataModels
{
    public class User : Entity
    {
        [Key]
        public long user_id { get; set; }
        public long address_id { get; set; }
        public string username { get; set; }
        public string password { get; set; }
        [StringLength(20)]
        public string phone_number { get; set; }
        [JsonIgnore]
        [ForeignKey(nameof(address_id))]
        public virtual Address Address { get; set; }

        [InverseProperty("User")]
        public virtual ICollection<Pet> Pets { get; set; }
    }
}