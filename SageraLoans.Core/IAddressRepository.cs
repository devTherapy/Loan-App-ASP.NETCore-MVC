using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using SageraLoans.Models;

namespace SageraLoans.Core
{
    
    public interface IAddressRepository
    {
         Task AddAddress(Address model);
    }
}
