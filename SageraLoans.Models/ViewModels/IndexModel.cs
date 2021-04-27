using System;
using System.Collections.Generic;
using System.Text;

namespace SageraLoans.Models.ViewModels
{
    public class IndexModel
    {
        public IEnumerable<LoanCompanyDetailsViewModel> details {get; set;}
        public IEnumerable<LoanCategoryViewModel> categories { get; set; }
    }
}
