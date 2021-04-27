using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace SageraLoans.Models.ViewModels
{
    public class RegisterViewModel
    {

        [Required]
        public string FirstName { get; set; }

        [Required]
        public string LastName { get; set; }

        [Required]
        [EmailAddress]
        public string Email { get; set; }

        [Required]
        //[DataType(DataType.Password)]
        public string Password { get; set; }

        [Required]
        //[DataType(DataType.Password)]
        [Display(Name = "Confirm Password")]
        [Compare("Password",ErrorMessage = "Password fields do not match")]
        public string ConfirmPassword { get; set; }

        [ValidateDob(18, 70, ErrorMessage = "user must be within 18 to 70 years of age")]
        [Display(Name = "Date of Birth")]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public DateTime Dob { get; set; }

        //public PhoneNumber PhoneNumber { get; set; }

        [Required(ErrorMessage = "You must provide a country code")]
        [Display(Name = "Country Code")]
        public string CountryCode { get; set; }

        [Required(ErrorMessage = "You must provide a phone number")]
        [Display(Name = "Phone Number")]
        [DataType(DataType.PhoneNumber)]
        [RegularExpression(@"^\(?([0-9]{3})\)?[-. ]?([0-9]{3})[-. ]?([0-9]{4})$", ErrorMessage = "Not a valid phone number")]
        public string Number { get; set; }

        public Address Address { get; set; }

        [Required]
        public string Gender { get; set; }

        [Required]
        public string PostalCode { get; set; }

        [Required] public string Street { get; set; } = "7 Asajon Way";

        [Required]
        public string City { get; set; }

        [Required]
        public string State { get; set; }

        [Required]
        public string Country { get; set; }

        [Required]
        [Display(Name = "I accept all terms and conditions")]
        public bool RememberMe { get; set; }
    }

    public class  ValidateDobAttribute:ValidationAttribute
    {

        //encapsulate user's age
        public int MinimumAge { get; private set; }
        public int MaximumAge { get; private set; }

          public ValidateDobAttribute(int minimumAge, int maximumAge )
          {
              this.MaximumAge = minimumAge;
              this.MaximumAge = maximumAge;
          }

          /// <summary>
          /// compare user's age and validate accordingly
          /// </summary>
          /// <param name="value"></param>
          /// <returns></returns>
        public override bool IsValid(object value)
        {
            MinimumAge = 18; MaximumAge = 70;

            //get user birthdate
            var birthDate = Convert.ToDateTime(value).Year;

            //get the present date

            var currentDate = DateTime.Today.Year;

            //calculate user's present age

            var userAge = currentDate - birthDate;

            //compare and validate user's age 

            if ((userAge < 18) || (userAge > MaximumAge))
            {
                return false;
            }

            else
            {
                return true;
            }
        }
    }
}
