using System.Collections.Generic;
using System.Threading.Tasks;
using SageraLoans.Models;

namespace SageraLoans.Core
{
    public interface ILoanCategoryRepository
    {
        /// <summary>
        /// Add Category to the database
        /// </summary>
        /// <param name="category"></param>
        /// <returns>number of changes effected in database</returns>
        Task<int> AddLoanCategory(LoanCategory category);

        /// <summary>
        /// Get Category from database
        /// </summary>
        /// <param name="id"></param>
        /// <returns>LoanCategory Entity</returns>
        Task<LoanCategory> GetLoanCategory(string id);

        /// <summary>
        /// Get List of categories from database
        /// </summary>
        /// <returns>LoanCategory Entity</returns>
        Task<List<LoanCategory>> GetAllLoanCategories();

        /// <summary>
        /// Update any change to category in database
        /// </summary>
        /// <param name="category"></param>
        /// <returns>number of changes effected in database</returns>
        Task<int> UpdateLoanCategory(LoanCategory category);

        /// <summary>
        /// Delete LoanCategory from the database
        /// </summary>
        /// <param name="id"></param>
        /// <returns>number of changes effected in the database</returns>
        Task<int> DeleteLoanCategory(string id);
    }
}