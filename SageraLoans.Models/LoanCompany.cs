using System;
using System.Collections.Generic;
using System.Text;

namespace SageraLoans.Models
{
    public class LoanCompany
    {
        public string Id { get; set; }
        public string CompanyName { get; set; }
        public string CompanyLogo { get; set; }
        public string Description { get; set; }
        public ICollection<Loan> Loans { get; set; }

        public LoanCompany()
        {
            Id = Guid.NewGuid().ToString();
        }
    }
}
