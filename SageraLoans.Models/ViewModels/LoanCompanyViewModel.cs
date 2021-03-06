using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;
using Microsoft.AspNetCore.Http;

namespace SageraLoans.Models.ViewModels
{
    public class LoanCompanyViewModel
    {
        public string Id { get; set; }

        [Required(ErrorMessage = "Please enter company name")]
        [Display(Name = "Company Name")]
        public string CompanyName { get; set; }
        
        [Display(Name = "Company Logo")]
        public string CompanyLogo { get; set; }

        [Required(ErrorMessage = "Please enter a description of the company")]
        public string Description { get; set; }
    }
}
