using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Hosting.Internal;
using SageraLoans.Core;
using SageraLoans.Models;
using SageraLoans.Models.ViewModels;
using Microsoft.AspNetCore.Authorization;

namespace SageraLoans.UI.Controllers
{
    public class LoanCompanyController : Controller
    {
        private readonly ILoanCompanyRepository _loanCompanyRepository;
        private readonly IWebHostEnvironment _webHostEnvironment;
        public LoanCompanyController(ILoanCompanyRepository loanCompanyRepository, IWebHostEnvironment webHostEnvironment)
        {
            _loanCompanyRepository = loanCompanyRepository;
            _webHostEnvironment = webHostEnvironment;
        }

        public async Task<IActionResult> Index()
        {
            var loanCompanies = await _loanCompanyRepository.GetAllLoanCompanies();
            var listOfLoanCompanyViewModels = new List<LoanCompanyViewModel>();
            foreach (var item in loanCompanies)
            {
                var viewModel = new LoanCompanyViewModel()
                {
                    Id = item.Id,
                    Description = item.Description,
                    CompanyLogo = item.CompanyLogo,
                    CompanyName = item.CompanyName
                };
                listOfLoanCompanyViewModels.Add(viewModel);
            }
            return View(listOfLoanCompanyViewModels);
        }

        // GET: LoanCompany/Create
        public async Task<IActionResult> AddOrEdit(string id = "")
        {

            var viewModel = new LoanCompanyUploadViewModel();
            if (id != "")
            {
                var model = await _loanCompanyRepository.GetLoanCompany(id);
                viewModel.CompanyName = model.CompanyName;
                viewModel.Id = model.Id;
                viewModel.Description = model.Description;
            }
            return View(viewModel);
        }

        // POST: LoanCompany/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> AddOrEdit(LoanCompanyUploadViewModel loanCompanyUploadView)
        {
            if (ModelState.IsValid)
            {
                string uniquePhotoFileName = null;
                string uploadsFolder = Path.Combine(_webHostEnvironment.WebRootPath, "images");
                uniquePhotoFileName = Guid.NewGuid().ToString() + "_" + loanCompanyUploadView.CompanyLogo.FileName;
                string filePath = Path.Combine(uploadsFolder, uniquePhotoFileName);

                await loanCompanyUploadView.CompanyLogo.CopyToAsync(new FileStream(filePath, FileMode.Create));

                if (loanCompanyUploadView.Id == null)
                {
                    var loanCompany = new LoanCompany()
                    {

                        Description = loanCompanyUploadView.Description,
                        CompanyLogo = uniquePhotoFileName,
                        CompanyName = loanCompanyUploadView.CompanyName
                    };
                    await _loanCompanyRepository.AddLoanCompany(loanCompany);
                }

                else
                {
                    var loanCompany = await _loanCompanyRepository.GetLoanCompany(loanCompanyUploadView.Id);

                    loanCompany.Description = loanCompanyUploadView.Description;
                    loanCompany.CompanyLogo = uniquePhotoFileName;
                    loanCompany.CompanyName = loanCompanyUploadView.CompanyName;
                    await _loanCompanyRepository.UpdateLoanCompany(loanCompany);
                }
                return RedirectToAction(nameof(Index));
            }
            return View(loanCompanyUploadView);
        }


        // GET: LoanCompany/Delete/5
        public async Task<IActionResult> Delete(string id)
        {
            await _loanCompanyRepository.DeleteLoanCompany(id);
            return RedirectToAction(nameof(Index));
        }




        [Authorize]
        [HttpGet]
        public async Task<IActionResult> Details(string Id = "")
        {
            var model = new LoanCompanyDetailsViewModel();

            if (Id != "")
            {
                var loanCompany = await _loanCompanyRepository.GetLoanCompany(Id);
                {
                    model.Id = loanCompany.Id;
                    model.CompanyName = loanCompany.CompanyName;
                    model.CompanyDescription = loanCompany.Description;
                    model.CompanyLoans = loanCompany.Loans;
                }


            }
            return View(model);
        }
    }
}
