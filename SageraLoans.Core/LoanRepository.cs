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
    public class LoanRepository : ILoanRepository
    {
        private readonly AppDbContext _context;
        public LoanRepository(AppDbContext context)
        {
            _context = context;
        }
        /// <summary>
        /// Add Loan to database
        /// </summary>
        /// <param name="loan"></param>
        /// <returns>number of affected rows</returns>
        public async Task<int> AddLoan(Loan loan)
        {
            await _context.Loans.AddAsync(loan);
            var result = await _context.SaveChangesAsync();
            return result;
        }

        /// <summary>
        /// Get a particular loan from a database
        /// </summary>
        /// <param name="id"></param>
        /// <returns>Loan</returns>
        public async Task<Loan> GetLoan(string id)
        {
            var query = await _context.Loans.Where(l => l.Id == id).Include(l => l.LoanCompany)
                .Include(l => l.LoanCategory).ToListAsync();
            var loan = query.Single();
            return loan;
        }

        /// <summary>
        /// Get the list of loans from the database
        /// </summary>
        /// <returns>IEnumerable<Loan></returns>
        public async Task<List<Loan>> GetAllLoans()
        {
            var loans = await _context.Loans.Include(l => l.LoanCompany)
                .Include(l => l.LoanCategory).ToListAsync();
            return loans;
        }

        /// <summary>
        /// Update the loan in the database
        /// </summary>
        /// <param name="loan"></param>
        /// <returns>number of rows affected</returns>
        public async Task<int> UpdateLoan(Loan loan)
        {
            _context.Loans.Update(loan);
            return await _context.SaveChangesAsync();
        }

        /// <summary>
        /// Delete a Loan from the database
        /// </summary>
        /// <param name="id"></param>
        /// <returns>number of rows affected</returns>
        public async Task<int> DeleteLoan(string id)
        {
            var loan = await _context.Loans.FindAsync(id);
            _context.Loans.Remove(loan);
            var result = await _context.SaveChangesAsync();
            return result;
        }
    }
}
