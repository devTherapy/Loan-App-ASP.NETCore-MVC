using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.AspNetCore.Identity;

namespace SageraLoans.Models
{
    public class AppUser : IdentityUser
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public DateTime? Dob { get; set; } = DateTime.Now;
        public ICollection<PhoneNumber> PhoneNumbers { get; set; }
        public ICollection<AppUserLoan> AppUserLoans { get; set; }
        public Address Address { get; set; }

    }
}