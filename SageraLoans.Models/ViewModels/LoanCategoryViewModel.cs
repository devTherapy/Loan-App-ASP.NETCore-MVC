using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace SageraLoans.Models.ViewModels
{
    public class LoanCategoryViewModel
    {
       
       public string Id { get; set; }
        
        [Required(ErrorMessage ="Please enter category name")]
        [MinLength(4)]
        [Display(Name ="Category Name")]
        public string CategoryName{ get; set; }

    }
}
