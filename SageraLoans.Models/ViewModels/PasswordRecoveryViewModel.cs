using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace SageraLoans.Models.ViewModels
{
    public class PasswordRecoveryViewModel
    {
        [Required]
        public string Email { get; set; }
    }
}
