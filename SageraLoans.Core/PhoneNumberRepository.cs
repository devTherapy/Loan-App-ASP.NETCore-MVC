using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using SageraLoans.Data.Data;
using SageraLoans.Models;

namespace SageraLoans.Core
{
    public class PhoneNumberRepository:IPhoneNumberRepository
    {
        private readonly AppDbContext _ctx;

        public PhoneNumberRepository(AppDbContext ctx)
        {
            _ctx = ctx;
        }
        public async Task AddPhoneNumber(PhoneNumber model)
        {
            var createPhone = await _ctx.PhoneNumbers.AddAsync(model);

            await _ctx.SaveChangesAsync();
        }
    }
}
