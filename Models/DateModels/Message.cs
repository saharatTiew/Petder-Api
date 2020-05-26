using System.ComponentModel.DataAnnotations;
using System;
using System.ComponentModel.DataAnnotations.Schema;
using Newtonsoft.Json;

namespace petder.Models.DataModels
{
    public class Message : Entity
    {
        [Key]
        public long message_id { get; set; }
        public long session_id { get; set; }
        public long sender_pet_id { get; set; }
        public string message { get; set; }

        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public DateTime sent_datetime { get; set; } = DateTime.UtcNow;
        public bool is_unsent { get; set; } = false;
        public bool is_system_message {get; set;}
        public DateTime? unsent_datetime { get; set; }

        [JsonIgnore]
        [ForeignKey(nameof(session_id))]
        public virtual Session Session { get; set; }
    }
}