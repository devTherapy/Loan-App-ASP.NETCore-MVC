using System.Collections.Generic;
using System.Threading.Tasks;
using SageraLoans.Models;

namespace SageraLoans.Core
{
    public interface ILoanRepository
    {
        /// <summary>
        /// Add Loan to database
        /// </summary>
        /// <param name="loan"></param>
        /// <returns>number of affected rows</returns>
        Task<int> AddLoan(Loan loan);

        /// <summary>
        /// Get a particular loan from a database
        /// </summary>
        /// <param name="id"></param>
        /// <returns>Loan</returns>
        Task<Loan> GetLoan(string id);

        /// <summary>
        /// Get the list of loans from the database
        /// </summary>
        /// <returns>IEnumerable<Loan></returns>
        Task<List<Loan>> GetAllLoans();

        /// <summary>
        /// Update the loan in the database
        /// </summary>
        /// <param name="loan"></param>
        /// <returns>number of rows affected</returns>
        Task<int> UpdateLoan(Loan loan);

        /// <summary>
        /// Delete a Loan from the database
        /// </summary>
        /// <param name="id"></param>
        /// <returns>number of rows affected</returns>
        Task<int> DeleteLoan(string id);
    }
}