using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using SageraLoans.Core;
using SageraLoans.Models;
using SageraLoans.Models.ViewModels;

namespace SageraLoans.UI.Controllers
{
    public class LoanCategoryController : Controller
    {
        private readonly ILoanCategoryRepository _loanCategoryRepository;

        public LoanCategoryController(ILoanCategoryRepository loanCategoryRepository)
        {
            _loanCategoryRepository = loanCategoryRepository;
        }

        public async Task<IActionResult> Index()
        {
            var loans = await _loanCategoryRepository.GetAllLoanCategories();
            var listOfLoanCategoryViewModels = new List<LoanCategoryViewModel>();
            foreach (var item in loans)
            {
                var viewModel = new LoanCategoryViewModel()
                {
                    Id = item.Id,
                    CategoryName = item.CategoryName
                };
                listOfLoanCategoryViewModels.Add(viewModel);
            }
            return View(listOfLoanCategoryViewModels);
        }

        // GET: LoanCategory/Create
        public async Task<IActionResult> AddOrEdit(string id = "")
        {

            var viewModel = new LoanCategoryViewModel();
            if (id != "")
            {
                var model = await _loanCategoryRepository.GetLoanCategory(id);
                viewModel.CategoryName = model.CategoryName;
                viewModel.Id = model.Id;
            }
            return View(viewModel);
        }

        // POST: LoanCategory/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> AddOrEdit(LoanCategoryViewModel loanCategoryView)
        {
            if (ModelState.IsValid)
            {
                if (loanCategoryView.Id == null)
                {
                    var loanCategory = new LoanCategory()
                    {
                        CategoryName = loanCategoryView.CategoryName
                    };
                    await _loanCategoryRepository.AddLoanCategory(loanCategory);
                }

                else
                {
                    var loanCategory = await _loanCategoryRepository.GetLoanCategory(loanCategoryView.Id);
                    loanCategory.CategoryName = loanCategoryView.CategoryName;
                    await _loanCategoryRepository.UpdateLoanCategory(loanCategory);
                }
                return RedirectToAction(nameof(Index));
            }
            return View(loanCategoryView);
        }


        // GET: LoanCategory/Delete/5
        public async Task<IActionResult> Delete(string id)
        {
            await _loanCategoryRepository.DeleteLoanCategory(id);
            return RedirectToAction(nameof(Index));
        }


    }
}
