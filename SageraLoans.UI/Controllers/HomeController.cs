using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using SageraLoans.Data.Data;
using SageraLoans.Models;
using SageraLoans.Models.ViewModels;
using Microsoft.AspNetCore.Identity;
using SageraLoans.Core;

namespace SageraLoans.UI.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly UserManager<AppUser> _userMgr;
        private readonly ILoanCategoryRepository _loanCategoryRepository;
        private readonly ILoanCompanyRepository _companyRepository;
        private readonly IEmailing _emailer;

        public HomeController(ILogger<HomeController> logger, UserManager<AppUser>UserManager, ILoanCategoryRepository categoryRepository, ILoanCompanyRepository companyRepository, IEmailing emailer)
        {
            _logger = logger;
            _userMgr = UserManager;
            _loanCategoryRepository = categoryRepository;
            _companyRepository = companyRepository;
            _emailer = emailer;
        }

        public async Task<IActionResult> Index(string Id)
        {
            await _emailer.sendEmail("physionode@gmail.com", "Welcome to decagon", "Decagon welcomes you");
            if (!string.IsNullOrWhiteSpace(Id))
            {               
                var user = await  _userMgr.FindByIdAsync(Id);

                ViewBag.UserName = user.UserName;
            }

            var categories = await _loanCategoryRepository.GetAllLoanCategories();
            var loanCompanies = await _companyRepository.GetAllLoanCompanies();
            var listOfCategoriesViewModel = new List<LoanCategoryViewModel>();
            var listOfLoanCompaniesDetailsViewModel = new List<LoanCompanyDetailsViewModel>();

            foreach (var item in categories)
            {
                var categoryViewModel = new LoanCategoryViewModel()
                {
                    CategoryName = item.CategoryName,
                    Id = item.Id
                };
                listOfCategoriesViewModel.Add(categoryViewModel);
            }
            
            foreach( var item in loanCompanies)
            {
                var loanCompanyVM = new LoanCompanyDetailsViewModel()
                {
                    CompanyDescription = item.Description,
                    CompanyName = item.CompanyName,
                    Id = item.Id


                };
                listOfLoanCompaniesDetailsViewModel.Add(loanCompanyVM);
            }

            var g = new IndexModel { details = listOfLoanCompaniesDetailsViewModel, categories = listOfCategoriesViewModel };
            return View(g);
        }

        public IActionResult Privacy()
        {
            return View();

        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
