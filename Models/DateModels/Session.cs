using System.ComponentModel.DataAnnotations;
using System;
using System.ComponentModel.DataAnnotations.Schema;
using System.Collections.Generic;
using Newtonsoft.Json;

namespace petder.Models.DataModels
{
    public class Session : Entity
    {
        [Key]
        public long session_id { get; set; }
        public long request_id { get; set; }

        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public DateTime create_datetime { get; set; } = DateTime.UtcNow;
        [JsonIgnore]
        [ForeignKey(nameof(request_id))]
        public virtual RequestList RequestList { get; set; }
        [JsonIgnore]
        [InverseProperty(nameof(Message.Session))]
        public virtual ICollection<Message> Messages { get; set; }
    }
}