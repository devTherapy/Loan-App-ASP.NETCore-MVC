using System;
using System.Collections.Generic;
using System.Text;

namespace SageraLoans.Models.ViewModels
{
    public class CompanyLoanViewModel
    {
        public string Id { get; set; }
        public string CompanyName { get; set; }
        public string CompanyDescription { get; set; }
        public ICollection<Loan> CompanyLoans { get; set; }
    }
}
