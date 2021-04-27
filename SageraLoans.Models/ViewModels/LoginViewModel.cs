using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace SageraLoans.Models.ViewModels
{
   public  class LoginViewModel
    {
        [Required]
        public string Email { get; set; }
        [Required]
        [DataType(DataType.Password)]
        public string Password { get; set; }
        public bool RemeberMe { get; set;}
        public string ReturnUrl { get; set; }

    }
}
