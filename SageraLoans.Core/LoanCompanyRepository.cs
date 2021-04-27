using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using SageraLoans.Data.Data;
using SageraLoans.Models;

namespace SageraLoans.Core
{

    public class LoanCompanyRepository : ILoanCompanyRepository
    {
        private readonly AppDbContext _context;
        public LoanCompanyRepository(AppDbContext context)
        {
            _context = context;
        }

        /// <summary>
        /// Add Loan Company to database
        /// </summary>
        /// <param name="company"></param>
        /// <returns>number of rows affected</returns>
        public async Task<int> AddLoanCompany(LoanCompany company)
        {
            await _context.LoanCompanies.AddAsync(company);
            var result = await _context.SaveChangesAsync();
            return result;
        }

        /// <summary>
        /// Get a Loan company from database
        /// </summary>
        /// <param name="id"></param>
        /// <returns>LoanCompany</returns>
        public async Task<LoanCompany> GetLoanCompany(string id)
        {
            var query = await _context.LoanCompanies.Where(l => l.Id == id).Include(l => l.Loans)
                .ThenInclude(l => l.LoanCategory).ToListAsync();
            var loanCompany = query.Single();
            return loanCompany;
        }

        /// <summary>
        /// Get all loan companies from database
        /// </summary>
        /// <returns>IEnumerable<LoanCompany></returns>
        public async Task<List<LoanCompany>> GetAllLoanCompanies()
        {
            var companies = await _context.LoanCompanies.ToListAsync();
            return companies;
        }

        /// <summary>
        /// Update details of a Loan company
        /// </summary>
        /// <param name="company"></param>
        /// <returns>number of rows affected</returns>
        public async Task<int> UpdateLoanCompany(LoanCompany company)
        {
            _context.LoanCompanies.Update(company);
            return await _context.SaveChangesAsync();
        }

        /// <summary>
        /// Delete Loan company
        /// </summary>
        /// <param name="id"></param>
        /// <returns>number of rows affected</returns>
        public async Task<int> DeleteLoanCompany(string id)
        {
            var company = await _context.LoanCompanies.FindAsync(id);
            _context.LoanCompanies.Remove(company);
            var result = await _context.SaveChangesAsync();
            return result;
        }
        /// <summary>
        /// Get all loan companies and their associated loans
        /// </summary>
        /// <returns></returns>
        public async Task<List<LoanCompany>> GetLoanComapnies()
        {
            var loanCompanies = await _context.LoanCompanies.Include(l => l.Loans)
                .ThenInclude(l => l.LoanCategory).ToListAsync();
            return loanCompanies;
        }

        //public async Task <LoanCompany> GetLoanCompany()
        //{
        //    var query = await _context.LoanCompanies.Include(l => l.Loans)
        //        .ThenInclude(l => l.LoanCategory).ToListAsync();
        //    var loanCompany = query.Single();
        //    return loanCompany;
        //}
    }

}
