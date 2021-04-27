using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace SageraLoans.Models.ViewModels
{
    public class LoanViewModel
    {
        public string Id { get; set; }

        [Required(ErrorMessage = "A loan must belong to  a loan category")]
        public string LoanCategoryId { get; set; }
        public LoanCategory LoanCategory { get; set; }

        [Required(ErrorMessage = "A loan must belong to a loan company")]
        public string LoanCompanyId { get; set; }
        public LoanCompany LoanCompany { get; set; }

        [Required(ErrorMessage = "please enter interest rate")]
        public string InterestRate { get; set; }
        [Required(ErrorMessage = "Please enter moratorium")]
        public string Moratorium { get; set; }
        [Required(ErrorMessage = "Please enter Minimum amount for loan")]
        [DataType(DataType.Currency)]
        public decimal MinimumAmount { get; set; }
        [Required(ErrorMessage = "Please enter Maximum amount for loan")]
        [DataType(DataType.Currency)]
        public decimal MaximumAmount { get; set; }
    }
}
