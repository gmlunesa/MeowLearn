using System.ComponentModel.DataAnnotations;

namespace MeowLearn.Models
{
    public class RegistrationModel
    {
        [Required]
        [StringLength(100)]
        [EmailAddress]
        public string Email { get; set; }

        [Required]
        [DataType(DataType.Password)]
        public string Password { get; set; }

        [Required]
        [Compare("Password")]
        [DataType(DataType.Password)]
        [Display(Name = "Confirm Password")]
        public string ConfirmPassword { get; set; }

        [Required]
        [StringLength(100, MinimumLength = 2)]
        [Display(Name = "First Name")]
        public string FirstName { get; set; }

        [Required]
        [StringLength(100, MinimumLength = 2)]
        [Display(Name = "Last Name")]
        public string LastName { get; set; }

        [Required]
        [StringLength(150, MinimumLength = 2)]
        public string Address { get; set; }

        [Required]
        [RegularExpression(
            "^[0-9]{4}$",
            ErrorMessage = "Please enter a valid Philippine zip code."
        )]
        [Display(Name = "Zip Code")]
        public string ZipCode { get; set; }

        [Required]
        [Display(Name = "Phone Number")]
        [RegularExpression(
            "^(09|\\+639)\\d{9}$",
            ErrorMessage = "Please enter a valid Philippine mobile number."
        )]
        public string PhoneNumber { get; set; }

        public bool AcceptUserAgreement { get; set; }

        public string RegistrationInvalid { get; set; }
    }
}
