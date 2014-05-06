using System.ComponentModel.DataAnnotations;

namespace _3vikna.Models
{
    public class ExternalLoginConfirmationViewModel
    {
        [Required]
        [Display(Name = "User name")]
        public string UserName { get; set; }
    }

    public class ManageUserViewModel
    {
        [Required]
        [DataType(DataType.Password)]
        [Display(Name = "Current password")]
        public string OldPassword { get; set; }

        [Required]
        [StringLength(100, ErrorMessage = "The {0} must be at least {2} characters long.", MinimumLength = 6)]
        [DataType(DataType.Password)]
        [Display(Name = "New password")]
        public string NewPassword { get; set; }

        [DataType(DataType.Password)]
        [Display(Name = "Confirm new password")]
        [Compare("NewPassword", ErrorMessage = "The new password and confirmation password do not match.")]
        public string ConfirmPassword { get; set; }
    }

    public class LoginViewModel
    {
        [Required]
        [Display(Name = "Notandanafn")]
        public string UserName { get; set; }

        [Required]
        [DataType(DataType.Password)]
        [Display(Name = "Lykilorð")]
        public string Password { get; set; }

        [Display(Name = "Viltu láta muna eftir þér?")]
        public bool RememberMe { get; set; }
    }

    public class RegisterViewModel
    {
        [Required]
        [Display(Name = "Notandanafn")]          // breytti Notandanafni ur User Name
        public string UserName { get; set; }

        [Required]
        [Display(Name = "Fullt nafn")]          // breytti Notandanafni ur User Name
        public string full_name { get; set; }

        [Required]
        [Display(Name = "Aldur")]          // breytti Notandanafni ur User Name
        public string age { get; set; }

        [Required]
        [DataType(DataType.EmailAddress)]
        [Display(Name = "email")]          // breytti Notandanafni ur User Name
        public string email { get; set; }

        [Required]
        [Display(Name = "Karl")]          // breytti Notandanafni ur User Name
        public bool Karl { get; set; }

        [Required]
        [StringLength(100, ErrorMessage = "The {0} must be at least {2} characters long.", MinimumLength = 6)]
        [DataType(DataType.Password)]
        [Display(Name = "Lykilorð")]
        public string Password { get; set; }

        [DataType(DataType.Password)]
        [Display(Name = "Endurtaka lykilorð")]
        [Compare("Password", ErrorMessage = "The password and confirmation password do not match.")]
        public string ConfirmPassword { get; set; }
    }
}
