using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace SageraLoans.Models.ViewModels
{
    public class PasswordResetViewModel
    {
        [Required]
        [DataType(DataType.Password)]
        public string Password { get; set; }
        [Required]
        [DataType(DataType.Password)]
        [Compare("Password", ErrorMessage="Passwords do not match")]
        public string ConfirmPassword { get; set; }
        [Required]
        public string Token { get; set; }
        [Required]
        public string Email { get; set; }
    }
}
