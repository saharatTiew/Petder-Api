using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace petder.Models.DataModels
{
    public class Breed : Entity
    {
        [Key]
        public long breed_id { get; set; }
        [StringLength(200)]
        public string name { get; set; }
        [InverseProperty("Breed")]
        public virtual ICollection<Pet> Pets { get; set; }
    }
}