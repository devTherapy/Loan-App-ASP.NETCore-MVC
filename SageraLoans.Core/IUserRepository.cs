using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using SageraLoans.Models;
using SageraLoans.Models.ViewModels;

namespace SageraLoans.Core
{
    public interface IUserRepository
    {
        Task<IdentityResult> SignUp(RegisterViewModel model);

        Task<IdentityResult> DeleteUser(string id);

        List<AppUser> GetAllUsers();
        Task<string> GetRole(AppUser user);
        Task<IdentityResult> RemoveRoleOfAdmin(AppUser user);
        Task<IdentityResult> AddRoleOfAdmin(AppUser user);
        Task<AppUser> GetUser(string id);
    } 
   
}
