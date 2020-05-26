using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace petder.Models.DataModels
{
    public class Address : Entity
    {
        [Key]
        public long address_id { get; set; }
        [StringLength(100)]
        public string name { get; set; }
        [InverseProperty(nameof(User.Address))]
        public ICollection<User> Users { get; set; }
    }
}