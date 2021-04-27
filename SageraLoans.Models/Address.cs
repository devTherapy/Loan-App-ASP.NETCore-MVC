using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace SageraLoans.Models
{
    public class Address
    {
        [Key]
        [ForeignKey("AppUser")]
        public string AppUserAddressId { get; set; }
        public AppUser AppUser { get; set; }
        public string Street { get; set; }
        public string City { get; set; }
        public string State { get; set; }
        public string Country { get; set; }
        public string PostalCode { get; set; }
    }
}
