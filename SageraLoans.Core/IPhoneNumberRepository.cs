using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using SageraLoans.Models;

namespace SageraLoans.Core
{
    public interface IPhoneNumberRepository
    {
        Task AddPhoneNumber(PhoneNumber model);
    }
}
