using Microsoft.AspNetCore.Identity;
using Newtonsoft.Json;
using SageraLoans.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SageraLoans.Data.Data
{
    public static class PreSeeder
    {
        /// <summary>
        /// Seeds the database with placeholder data
        /// </summary>
        /// <param name="ctx"></param>
        /// <param name="roleManager"></param>
        /// <param name=""></param>
        public static async Task Seed(AppDbContext ctx, RoleManager<IdentityRole> roleManager, UserManager<AppUser> userManager)
        {
            //Ensures the database is created.
            await ctx.Database.EnsureCreatedAsync();
            var roles = new List<string>() { "Admin", "Member", "Visitor" };
            if (!roleManager.Roles.Any())
            {
                for (int i = 0; i < roles.Count; i++)
                {
                    var role = new IdentityRole(roles[i]);
                    await roleManager.CreateAsync(role);
                }
            }


            //Read all the data from JSON.

            //var streamCompanyData = new StreamReader(@"Company.json");
            var companyData = await System.IO.File.ReadAllTextAsync("../SageraLoans.Data/Data/Company.json");
            
            //var streamUserData = new StreamReader(@"User.json");
            var userData = await System.IO.File.ReadAllTextAsync("../SageraLoans.Data/Data/User.json");

            //var streamloanData = new StreamReader(@"LoanCategory.json");
            var loanCategoryData = await System.IO.File.ReadAllTextAsync("../SageraLoans.Data/Data/LoanCategory.json");


            //deserialized objects
            var users = JsonConvert.DeserializeObject<List<AppUser>>(userData);
            var companies = JsonConvert.DeserializeObject<List<LoanCompany>>(companyData);
            var loanCategories = JsonConvert.DeserializeObject<List<LoanCategory>>(loanCategoryData);


            //create user
            if (!userManager.Users.Any())
            {
                try
                {
                    string userType;
                    int counter = 1;
                    foreach (var user in users)
                    {
                        if (counter < 2)
                        {
                            userType = "Admin";
                            var result = await userManager.CreateAsync(user, "P@SSw0rd");
                            if (!result.Succeeded)
                            {
                                HandlePreSeederError(result, userType);
                            }
                            await userManager.AddToRoleAsync(user, "Admin");
                        }

                        if (counter >= 2 && counter < 6)
                        {
                            userType = "Member";
                            var result = await userManager.CreateAsync(user, "P@SSw0rd");
                            if (!result.Succeeded)
                            {
                                HandlePreSeederError(result, userType);
                            }
                            await userManager.AddToRoleAsync(user, "Member");
                        }

                        else if (counter >= 6)
                        {
                            userType = "Visitor";
                            var result = await userManager.CreateAsync(user, "ProUser@t40");
                            if (!result.Succeeded)
                            {
                                HandlePreSeederError(result, userType);
                            }
                            await userManager.AddToRoleAsync(user, "Visitor");
                        }
                        counter++;
                    }
                }
                catch (Exception e)
                {

                    throw new Exception(e.Message);
                }
            }

            //Add loan category

            if (!ctx.LoanCategories.Any())
            {
                foreach (var category in loanCategories)
                {
                    await ctx.LoanCategories.AddAsync(category);
                    await ctx.SaveChangesAsync();

                }
            }


            //add loan company

            if (!ctx.LoanCompanies.Any())
            {
                foreach (var company in companies)
                {
                    await ctx.LoanCompanies.AddAsync(company);
                    await ctx.SaveChangesAsync();
                }

            }

        }

        /// <summary>
        /// Show errors if issues when adding user to database
        /// </summary>
        /// <param name="result"></param>
        /// <param name="userType"></param>
        private static void HandlePreSeederError(IdentityResult result, string userType)
        {
            var errMsg = "";
            foreach (var error in result.Errors)
            {
                errMsg += error.Description;
            }
            throw new Exception($"failed to add {userType}");
        }
    }
}