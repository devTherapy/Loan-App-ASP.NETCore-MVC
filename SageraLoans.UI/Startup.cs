using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using SageraLoans.Core;
using SageraLoans.Data.Data;
using SageraLoans.Models;
using SageraLoans.UI.Extensions;

namespace SageraLoans.UI
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddControllersWithViews();
            services.AddDbContextPool<AppDbContext>(
                options => options.UseSqlite(Configuration.GetConnectionString("Conn")));
            services.AddIdentity<AppUser, IdentityRole>().AddEntityFrameworkStores<AppDbContext>().AddDefaultTokenProviders();
            services.AddScoped<IAddressRepository, AddressRepository>();
            services.AddScoped<IPhoneNumberRepository, PhoneNumberRepository>();
            services.AddScoped<IUserRepository, UserRepository>();
            services.AddScoped<IEmailServiceRepository, EmailServiceRepository>();


            services.AddScoped<ILoanCategoryRepository, LoanCategoryRepository>();
            services.AddScoped<ILoanRepository, LoanRepository>();
            services.AddScoped<ILoanCompanyRepository, LoanCompanyRepository>();
            services.ConfigureMailing(Configuration);
           

        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env, AppDbContext ctx, RoleManager<IdentityRole> roleManager, UserManager<AppUser> userManager )
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseStatusCodePagesWithReExecute("/Error/{0}");
                app.UseExceptionHandler("/Error");
                
            }
            app.UseStaticFiles();

            app.UseRouting();


            app.UseAuthentication();
            app.UseAuthorization();


            PreSeeder.Seed(ctx, roleManager, userManager).Wait();


            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller=Home}/{action=Index}/{id?}");
            });
        }
    }
}
