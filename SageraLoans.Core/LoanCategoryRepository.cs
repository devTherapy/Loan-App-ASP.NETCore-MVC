using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using SageraLoans.Data.Data;
using SageraLoans.Models;

namespace SageraLoans.Core
{
    public class LoanCategoryRepository : ILoanCategoryRepository
    {
        private readonly AppDbContext _context;
        public LoanCategoryRepository(AppDbContext context)
        {
            _context = context;
        }

        /// <summary>
        /// Add Category to the database
        /// </summary>
        /// <param name="category"></param>
        /// <returns>number of changes effected in database</returns>
        public async Task<int> AddLoanCategory(LoanCategory category)
        {
            await _context.LoanCategories.AddAsync(category);
            var result = await _context.SaveChangesAsync();
            return result;
        }

        /// <summary>
        /// Get Category from database
        /// </summary>
        /// <param name="id"></param>
        /// <returns>LoanCategory Entity</returns>
        public async Task<LoanCategory> GetLoanCategory(string id)
        {
            var category = await _context.LoanCategories.FindAsync(id);
            return category;
        }

        /// <summary>
        /// Get List of categories from database
        /// </summary>
        /// <returns>IEnumerable<LoanCategory></returns>
        public async Task<List<LoanCategory>> GetAllLoanCategories()
        {
            var categories = await _context.LoanCategories.ToListAsync();
            return categories;
        }

        /// <summary>
        /// Update any change to category in database
        /// </summary>
        /// <param name="category"></param>
        /// <returns>number of changes effected in database</returns>
        public async Task<int> UpdateLoanCategory(LoanCategory category)
        {
            _context.LoanCategories.Update(category);
            return await _context.SaveChangesAsync();
        }

        /// <summary>
        /// Delete LoanCategory from the database
        /// </summary>
        /// <param name="id"></param>
        /// <returns>number of changes effected in the database</returns>
        public async Task<int> DeleteLoanCategory(string id)
        {
            var categoryToremove = await _context.LoanCategories.FindAsync(id);
            _context.LoanCategories.Remove(categoryToremove);
            return await _context.SaveChangesAsync();
        }
     }

}
