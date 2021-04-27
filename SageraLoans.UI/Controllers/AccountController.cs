using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http.Extensions;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Logging;
using SageraLoans.Core;
using SageraLoans.Data.Data;
using SageraLoans.Models;
using SageraLoans.Models.ViewModels;

namespace SageraLoans.UI.Controllers
{

    public class AccountController : Controller
    {
        private readonly UserManager<AppUser> _userManager;
        private readonly SignInManager<AppUser> _signInManager;
        private readonly IAddressRepository _addressRepository;
        private readonly IUserRepository _userRepository;
        private readonly IPhoneNumberRepository _phoneNumberRepository;
        private readonly IEmailServiceRepository _emailService;

        // for logging
        private readonly ILogger<AccountController> _logger;

        public AccountController(AppDbContext context,UserManager<AppUser> userManager, SignInManager<AppUser> signInManager,
            IAddressRepository addressRepository,IUserRepository userRepository,IPhoneNumberRepository phoneNumberRepository,
            IEmailServiceRepository emailService, ILogger<AccountController> logger)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _addressRepository = addressRepository;
            _userRepository = userRepository;
            _phoneNumberRepository = phoneNumberRepository;
            _emailService = emailService;
            _logger = logger;
        }


        [HttpGet]
        public IActionResult Register()
        {
            return View();
        }


        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Register(RegisterViewModel model)
        {
            //check if validations provided are valid
            if (ModelState.IsValid)
            {

                //create new user
                 var newUser = await _userRepository.SignUp(model);

                 if (!newUser.Succeeded)
                 {
                     foreach (var err in newUser.Errors)
                     {
                         ModelState.AddModelError("",err.Description);
                         //log error
                         _logger.LogDebug(err.Code + ":" + err.Description );
                     }

                     return View(model);
                 }


                var userObj = await _userManager.FindByEmailAsync(model.Email);

                var userAddress = new Address()
                {
                    PostalCode = model.PostalCode,
                    Street = model.Street,
                    City = model.City,
                    State = model.State,
                    Country = model.Country,
                    AppUserAddressId = userObj.Id
                };

                // create user address
                await _addressRepository.AddAddress(userAddress);
               
                //phone Number model
                var userPhone = new PhoneNumber()
                {
                    CountryCode = model.CountryCode,
                    Number = model.Number,
                    PhoneNumberId = userObj.Id
                };

                //add user phone number
                await _phoneNumberRepository.AddPhoneNumber(userPhone);
              

                var user = new AppUser()
                {
                    FirstName = model.FirstName,
                    LastName = model.LastName,
                    UserName = model.Email,
                    Email = model.Email,
                    //Address = userAddress,
                    PasswordHash = model.Password,
                };

                //generate a token
                var token = await _userManager.GenerateEmailConfirmationTokenAsync(user);

               //generate a confirmation link

               var confirmationLink = Url.Action("ConfirmEmail", "Account", new {user.Email, token}, Request.Scheme);

               

               //send link to user email upon registration
               bool emailResponse = _emailService.SendEmailToConfirm(user.Email, confirmationLink);
               
                //checks if response is true;
               if (emailResponse)
               {
                   ViewBag.Message = "Registration Successful." +
                                     "please confirm the Email sent to your mail";

                   return View();
               }

               ViewBag.Message = "Registration failed";

               return View();



            }
            return View(model);
        }

        [HttpGet]
        [AllowAnonymous]
        public IActionResult ConfirmEmail()
        {
            return View();
        }


        [HttpPost]
        [AllowAnonymous]
        public async Task<IActionResult> ConfirmEmail(string email, string token)
        {
            //gets user email to confirm
            var user = await _userManager.FindByEmailAsync(email);

            //checks if user email exist(i.e if user email is not a fraud)
            if (user == null)
            {
                return View("Error");
            }
               

            //confirms user email
            var result = await _userManager.ConfirmEmailAsync(user, token);
            //returns confirm email view if successful and erroe view if not
            return View(result.Succeeded ? "ConfirmEmail" : "Error");
        }

        //error view action method
        public IActionResult Error()
        {
            return View();
        }


// Login start
        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Login(LoginViewModel model)

        {
            if (!ModelState.IsValid) return View(model);
            var user = await _userManager.FindByEmailAsync(model.Email);
            if (user == null)
            {
                ModelState.AddModelError("", "Invalid Credentials");
                return View(model);
            }

            var result = await _signInManager.PasswordSignInAsync(user, model.Password, model.RemeberMe, false);

            if (!result.Succeeded)
            {
                ModelState.AddModelError("", "Invalid Credentials");
                return View(model);
            }
            else
            {
                if(!string.IsNullOrEmpty(model.ReturnUrl))
                {
                    return LocalRedirect(model.ReturnUrl);
                }
                else
                {
                    return RedirectToAction("Index", "Home", new { user.Id });

                }
            }
        }
        public IActionResult PasswordRecovery()
        {

            return View();
        }
        [HttpPost]
        public async  Task<IActionResult> PasswordRecovery(PasswordRecoveryViewModel model)
        {
            if (!ModelState.IsValid) return View(model);
            var user = await _userManager.FindByEmailAsync(model.Email);
            if (user != null)
            {
                var token = await _userManager.GeneratePasswordResetTokenAsync(user);
                var link = Url.Action("PasswordReset", "Account", new { token, user.Email }, Request.Scheme);
                bool success =  _emailService.SendEmailToReset(model.Email, link);

                if (!success)
                {
                    ModelState.AddModelError("", "unable to send email for password reset");
                    return View(model);
                }

            }

            return RedirectToAction("PasswordRecoveryResponse", "Account");
        }

        public IActionResult PasswordRecoveryResponse()
        {
            return View();
        }



        public IActionResult PasswordReset(string token, string email)
        {

            if (string.IsNullOrWhiteSpace(token))
            {
                ViewBag.TokenError = "Token must be present for this process to take place";
            }

            if (string.IsNullOrWhiteSpace(email))
            {
                ViewBag.EmailError = " Email must be present for this process to take place";
            }
            ViewBag.Email = email;
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> PasswordReset(PasswordResetViewModel model)
        {
            if (!ModelState.IsValid) return View(model);
            var user = await _userManager.FindByEmailAsync(model.Email);
            if (user == null)
            {
                ModelState.AddModelError("", $"User with the email {model.Email} does not exist");
                return View(model);
            }
            var result = await _userManager.ResetPasswordAsync(user, model.Token, model.Password);
            if (!result.Succeeded)
            {
                foreach (var err in result.Errors)
                {
                    ModelState.AddModelError("", err.Description);
                }
                return View(model);
            }
            return RedirectToAction("PasswordConfirmationPage", "Account");
        }

        public IActionResult PasswordConfirmationPage()
        {
            return View();
        }

        public async Task<IActionResult> Logout()
        {
            await _signInManager.SignOutAsync();
            return RedirectToAction("Index", "Home");
        }

        public IActionResult BackToLogin()
        {
            return RedirectToAction("Login", "Account");
        }
    }

}
