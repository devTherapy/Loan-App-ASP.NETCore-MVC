using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;
using Microsoft.AspNetCore.Http;

namespace SageraLoans.Models.ViewModels
{
    public class LoanCompanyUploadViewModel
    {
        
        public string Id { get; set; }

        [Required(ErrorMessage = "Please enter company name")]
        [Display(Name = "Company Name")]
        public string CompanyName { get; set; }
        [Required(ErrorMessage = "Please upload company logo")]
        [Display(Name = "Company Logo")]
        public IFormFile CompanyLogo { get; set; }

        [Required(ErrorMessage = "Please enter a description of the company")]
        public string Description { get; set; }
    }
}
