using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using SageraLoans.Models;

namespace SageraLoans.Data.Data
{
    public class AppDbContext : IdentityDbContext<AppUser>
    {
        public AppDbContext(DbContextOptions<AppDbContext> options)
            : base(options)
        { }
        public DbSet<Address> AppUserAddress { get; set; }
        public DbSet<PhoneNumber> PhoneNumbers { get; set; }
        public DbSet<Loan> Loans { get; set; }
        public DbSet<LoanCategory> LoanCategories { get; set; }
        public DbSet<LoanCompany> LoanCompanies { get; set; }
        public DbSet<AppUserLoan> AppUserLoans { get; set; }

        public Task FindAsync(Address model)
        {
            throw new NotImplementedException();
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            //call to the parent’s OnModelCreating
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<AppUserLoan>()
                .HasKey(o => new { o.AppUserId, o.LoanId });
        }

    }
}