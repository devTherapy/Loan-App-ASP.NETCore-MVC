using System;
using System.Collections.Generic;
using System.Text;

namespace SageraLoans.Models
{
    public class Loan
    {
        public string Id { get; set; }
        public string LoanCategoryId { get; set; }
        public LoanCategory LoanCategory { get; set; }
        public string LoanCompanyId { get; set; }
        public LoanCompany LoanCompany { get; set; }
        public string InterestRate { get; set; }
        public string Moratorium { get; set; }
        public decimal MinimumAmount { get; set; }
        public decimal MaximumAmount { get; set; }
        public ICollection<AppUserLoan> AppUserLoans { get; set; }

        public Loan()
        {
            Id = Guid.NewGuid().ToString();
        }
    }
}