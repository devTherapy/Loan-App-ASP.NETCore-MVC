using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace SageraLoans.Core
{
    public interface IEmailServiceRepository
    {
        bool SendEmailToConfirm(string userEmail, string confirmationLink);
        bool SendEmailToReset(string userEmail, string Link);
    }
}
