using System.Collections.Generic;
using System.Threading.Tasks;
using SageraLoans.Models;

namespace SageraLoans.Core
{
    public interface ILoanCompanyRepository
    {
        /// <summary>
        /// Add a new Loan company to the database
        /// </summary>
        /// <param name="company"></param>
        /// <returns>number of rows affected</returns>
        Task<int> AddLoanCompany(LoanCompany company);

        /// <summary>
        /// Get a loan company from the database by Id
        /// </summary>
        /// <param name="id"></param>
        /// <returns>Loan company</returns>
        Task<LoanCompany> GetLoanCompany(string id);

        /// <summary>
        /// Get All Loan companies
        /// </summary>
        /// <returns>IEnumerable<LoanCompany></returns>
        Task<List<LoanCompany>> GetAllLoanCompanies();

        /// <summary>
        /// Update a loan company
        /// </summary>
        /// <param name="company"></param>
        /// <returns>number of rows affected</returns>
        Task<int> UpdateLoanCompany(LoanCompany company);

        /// <summary>
        /// Delete a Loan company
        /// </summary>
        /// <param name="id"></param>
        /// <returns>number of rows affected</returns>
        Task<int> DeleteLoanCompany(string id);
    }
}