using System.ComponentModel.DataAnnotations;
using System;
using System.ComponentModel.DataAnnotations.Schema;
using System.Collections.Generic;

namespace petder.Models.DataModels
{
    public class RequestList : Entity
    {
        [Key]
        public long request_list_id { get; set; }
        public long pet_id { get; set; }
        public long requested_pet_id { get; set; }
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public DateTime request_datetime { get; set; } = DateTime.UtcNow;
        public long status_id { get; set; }
        [ForeignKey(nameof(status_id))]
        public virtual Status Status { get; set; }
        [ForeignKey("pet_id")]
        public virtual Pet RequesterPet { get; set; }
        [ForeignKey("requested_pet_id")]
        public virtual Pet RequestedPet { get; set; }
        [InverseProperty(nameof(Session.RequestList))]
        public virtual ICollection<Session> Sessions { get; set; }

    }
}