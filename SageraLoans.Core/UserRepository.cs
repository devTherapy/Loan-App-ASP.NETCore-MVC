using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using SageraLoans.Models;
using SageraLoans.Models.ViewModels;

namespace SageraLoans.Core
{
    public  class UserRepository:IUserRepository
    {
        //interface initiation for injection
        private readonly UserManager<AppUser> _userManager;

        //injecting interfaces
        public UserRepository(UserManager<AppUser> userManager)
        {
            _userManager = userManager;
        }

        /// <summary>
        /// creates a new user
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public async Task<IdentityResult> SignUp(RegisterViewModel model)
        {

            //find user in database
            var findUser = await _userManager.FindByEmailAsync(model.Email);

            //checks if user exist
            if (findUser != null)
            {
                //Console.Write("user already exist");
            }

            var user = new AppUser
            {
                FirstName = model.FirstName,
                LastName = model.LastName,
                UserName = model.Email,
                Email = model.Email,
                PasswordHash = model.Password,
            };

            //creates user 
            var createUser =await _userManager.CreateAsync(user, model.Password);

            //if (!createUser.Succeeded)
            //{
            //    foreach (var error in createUser.Errors)
            //    {
            //    }
            //}

            ////add member role to user
            //await _userManager.AddToRoleAsync(user, "regularUser"); 

            return createUser;
        }

        public List<AppUser> GetAllUsers()
        {
            var result = _userManager.Users.ToList();
            return result;
        }

        public async Task<AppUser> GetUser(string id)
        {
            var user = await _userManager.FindByIdAsync(id);
            return user;
        }

        public async Task<string> GetRole(AppUser user)
        {
            var roles = await _userManager.GetRolesAsync(user);
            return  roles.FirstOrDefault();
        }

        public async Task<IdentityResult> RemoveRoleOfAdmin(AppUser user)
        {
            var result = await _userManager.RemoveFromRoleAsync(user, "Admin");
            return result;
        }

        public async Task<IdentityResult> AddRoleOfAdmin(AppUser user)
        {
            var result = await _userManager.AddToRoleAsync(user, "Admin");
            return result;
        }
        public async Task<IdentityResult> DeleteUser(string id)
        {
            var user = await _userManager.FindByIdAsync(id);
            if (user != null)
            {
                if (await _userManager.IsInRoleAsync(user, "Admin"))
                {
                    return IdentityResult.Failed(new IdentityError(){Code = "001", Description = "User has a role of admin and cannot be deleted"});
                }
                return await _userManager.DeleteAsync(user);
            }
            else
            {
                return IdentityResult.Failed(new IdentityError(){Code = "002", Description = "User not found"});

            }
        }

       
    }
}
