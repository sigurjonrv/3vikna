using System.ComponentModel.DataAnnotations;

namespace _3vikna.Models
{
    public class ExternalLoginConfirmationViewModel
    {
        [Required]
        [Display(Name = "Notandanafn")]
        public string UserName { get; set; }
    }

    public class ManageUserViewModel
    {
        [Required]
        [DataType(DataType.Password)]
        [Display(Name = "Núverandi lykilorð")]
        public string OldPassword { get; set; }

        [Required]
        [StringLength(100, ErrorMessage = "{0} verður að vera minnst {2} stafir að lengd.", MinimumLength = 6)]
        [DataType(DataType.Password)]
        [Display(Name = "Nýtt lykilorð")]
        public string NewPassword { get; set; }

        [DataType(DataType.Password)]
        [Display(Name = "Staðfest lykilorð")]
        [Compare("NewPassword", ErrorMessage = "Lykilorð og staðfest lykilorð passa ekki saman.")]
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
        [Display(Name = "Notandanafn")]         
        public string UserName { get; set; }

        [Required]
        [Display(Name = "Fullt nafn")]         
        public string full_name { get; set; }

        [Required]
        [Display(Name = "Aldur")]         
        public string age { get; set; }

        [Required]
        [DataType(DataType.EmailAddress)]
        [Display(Name = "email")]          
        public string email { get; set; }

        [Required]
        [Display(Name = "Karl")]         
        public bool Karl { get; set; }

        [Required]
        [StringLength(100, ErrorMessage = "{0} verður að vera minnst {2} stafir að lengd.", MinimumLength = 6)]
        [DataType(DataType.Password)]
        [Display(Name = "Lykilorð")]
        public string Password { get; set; }

        [DataType(DataType.Password)]
        [Display(Name = "Staðfest lykilorð")]
        [Compare("Password", ErrorMessage = "Lykilorð og staðfest lykilorð passa ekki saman.")]
        public string ConfirmPassword { get; set; }
    }
}
