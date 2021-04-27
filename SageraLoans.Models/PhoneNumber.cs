using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace SageraLoans.Models
{
    public class PhoneNumber
    {
        
        [ForeignKey("AppUser")]
        public string PhoneNumberId { get; set; }
        public AppUser AppUser { get; set; }
        public string CountryCode { get; set; }
        public string Number { get; set; }

    }

}