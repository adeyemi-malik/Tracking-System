using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace TrackingSystem.Models.AccountViewModels
{
    public class RegisterViewModel
    {
        [Required]
        [StringLength(maximumLength: 250, MinimumLength = 3, ErrorMessage = "The {0} must be at least {2} and at max {1} characters long.")]
        [Display(Name = "UserName")]
        public string UserName { get; set; }

        [Required]
        [StringLength(maximumLength: 250, MinimumLength = 3, ErrorMessage = "The {0} must be at least {2} and at max {1} characters long.")]
        [Display(Name = "FirstName")]
        public String FirstName { get; set; }

        [Required]
        [StringLength(maximumLength: 250, MinimumLength = 3, ErrorMessage = "The {0} must be at least {2} and at max {1} characters long.")]
        [Display(Name = "LastName")]
        public string LastName { get; set; }

        [MaxLength(250, ErrorMessage = "The {0} must be at max {1} characters long.")]
        [Display(Name = "OtherName")]
        public string OtherName { get; set; }

        [MaxLength(250, ErrorMessage = "The {0} must be at max {1} characters long.")]
        public string Address { get; set; }

        [Required]
        [Phone]
        public string Phone { get; set; }


        [Required]
        [EmailAddress]
        [Display(Name = "Email")]
        public string Email { get; set; }

        [Required]
        [StringLength(100, ErrorMessage = "The {0} must be at least {2} and at max {1} characters long.", MinimumLength = 6)]
        [DataType(DataType.Password)]
        [Display(Name = "Password")]
        public string Password { get; set; }

        [DataType(DataType.Password)]
        [Display(Name = "Confirm password")]
        [Compare("Password", ErrorMessage = "The password and confirmation password do not match.")]
        public string ConfirmPassword { get; set; }
    }
}
