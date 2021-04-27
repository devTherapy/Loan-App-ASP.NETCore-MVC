using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace SageraLoans.Models
{
    public class AppUserLoan
    {
        [Key]
        [Column(Order = 1)]
        [ForeignKey("AppUser")]
        public string AppUserId { get; set; }
        public AppUser AppUser { get; set; }

        [Key]
        [Column(Order = 2)]
        [ForeignKey("Loan")]
        public string LoanId { get; set; }
        public Loan Loan { get; set; }

    }
}
