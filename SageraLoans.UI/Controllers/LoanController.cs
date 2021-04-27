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
    public class LoanController : Controller
    {
        private readonly ILoanRepository _loanRepository;
        private readonly ILoanCategoryRepository _loanCategoryRepository;
        private readonly ILoanCompanyRepository _loanCompanyRepository;
        public LoanController(ILoanRepository loanRepository, ILoanCompanyRepository loanCompanyRepository, ILoanCategoryRepository loanCategoryRepository)
        {
            _loanRepository = loanRepository;
            _loanCompanyRepository = loanCompanyRepository;
            _loanCategoryRepository = loanCategoryRepository;

        }
        public async Task<IActionResult> Index()
        {
            var loans = await _loanRepository.GetAllLoans();

            var listOfLoanViewModels = new List<LoanViewModel>();
            foreach (var item in loans)
            {
                var viewModel = new LoanViewModel()
                {
                    Id = item.Id,
                    LoanCategory = item.LoanCategory,
                    LoanCompany = item.LoanCompany,
                    InterestRate = item.InterestRate,
                    MaximumAmount = item.MaximumAmount,
                    MinimumAmount = item.MinimumAmount,
                    Moratorium = item.Moratorium
                };
                listOfLoanViewModels.Add(viewModel);
            }

            return View(listOfLoanViewModels);
        }

        // GET: LoanCompany/Create
        public async Task<IActionResult> AddOrEdit(string id = "")
        {

            var viewModel = new LoanViewModel();
            if (id != "")
            {
                var model = await _loanRepository.GetLoan(id);
                viewModel.LoanCompany = model.LoanCompany;
                viewModel.LoanCategory = model.LoanCategory;
                viewModel.Id = model.Id;
                viewModel.InterestRate = model.InterestRate;
                viewModel.MaximumAmount = model.MaximumAmount;
                viewModel.MinimumAmount = model.MinimumAmount;
                viewModel.Moratorium = model.Moratorium;
            }

            ViewBag.loanCategories = await _loanCategoryRepository.GetAllLoanCategories();
            ViewBag.loanCompanies = await _loanCompanyRepository.GetAllLoanCompanies();
            return View(viewModel);
        }

        // POST: LoanCompany/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> AddOrEdit(LoanViewModel loanView)
        {
            if (ModelState.IsValid)
            {
                if (loanView.Id == null)
                {
                    var loan = new Loan()
                    {
                        LoanCategoryId = loanView.LoanCategoryId,
                        LoanCompanyId = loanView.LoanCompanyId,
                        InterestRate = loanView.InterestRate,
                        MaximumAmount = loanView.MaximumAmount,
                        MinimumAmount = loanView.MinimumAmount,
                        Moratorium = loanView.Moratorium,

                    };
                    await _loanRepository.AddLoan(loan);
                }

                else
                {
                    var loan = await _loanRepository.GetLoan(loanView.Id);

                    loan.LoanCompanyId = loanView.LoanCompanyId;
                    loan.LoanCategoryId = loanView.LoanCategoryId;
                    loan.InterestRate = loanView.InterestRate;
                    loan.MinimumAmount = loanView.MinimumAmount;
                    loan.MaximumAmount = loanView.MaximumAmount;
                    loan.Moratorium = loanView.Moratorium;
                    await _loanRepository.UpdateLoan(loan);
                }
                return RedirectToAction(nameof(Index));
            }
            return View(loanView);
        }

        // GET: LoanCompany/Delete/5
        public async Task<IActionResult> Delete(string id)
        {
            await _loanRepository.DeleteLoan(id);
            return RedirectToAction(nameof(Index));
        }
    }
}
