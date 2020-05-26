using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace petder.Models.DataModels
{
    public class Status : Entity
    {
        [Key]
        public long status_id { get; set; }
        [StringLength(200)]
        public string name { get; set; }
        [InverseProperty(nameof(RequestList.Status))]
        public ICollection<RequestList> RequestLists { get; set; }
    }
}