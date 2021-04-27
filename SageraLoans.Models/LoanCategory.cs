using System;
using System.Collections.Generic;
using System.Text;

namespace SageraLoans.Models
{
    public class LoanCategory
    {
        public LoanCategory()
        {
            Id = Guid.NewGuid().ToString();
        }
        public string Id { get; set; }
        public string CategoryName { get; set; }
    }
}
