using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using SageraLoans.Data.Data;
using SageraLoans.Models;

namespace SageraLoans.Core
{
    public class AddressRepository:IAddressRepository
    {
        //interface injections 
        private readonly AppDbContext _ctx;
        private readonly UserManager<AppUser> _userManager;

        public AddressRepository(AppDbContext context, UserManager<AppUser> userManager)
        {
            _ctx = context;
            _userManager = userManager;
        }

        /// <summary>
        /// Adds user address to the address table
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public async Task AddAddress(Address model)
        {
            try
            {
                //retrieves user
                var checkAddress = await _ctx.AppUserAddress.FindAsync(model);


                //checks if address already exist
                if (checkAddress != null)
                {
                    Console.WriteLine("Address already exist");
                }


                //adds populates user address table
                var createAddress = await _ctx.AppUserAddress.AddAsync(model);

                //saves changes

                await _ctx.SaveChangesAsync();
            }
            catch (Exception)
            {

                //ignored
            }
        }
    }
}
