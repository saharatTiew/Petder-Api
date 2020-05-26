using System.ComponentModel.DataAnnotations;
using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace petder.Models.DataModels
{
    public class BlockList : Entity
    {
        [Key]
        public long block_list_id { get; set; }
        public long pet_id { get; set; }
        public long blocked_pet_id { get; set; }
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public DateTime block_datetime { get; set; } = DateTime.UtcNow;
        [ForeignKey("pet_id")]
        public virtual Pet BlockerPet { get; set; }
        [ForeignKey("blocked_pet_id")]
        public virtual Pet BlockedPet { get; set; }
    }
}