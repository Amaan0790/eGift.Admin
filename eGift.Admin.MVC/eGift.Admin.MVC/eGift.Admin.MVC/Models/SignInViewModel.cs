using System.ComponentModel.DataAnnotations;

namespace eGift.Admin.MVC.Models
{
    public class SignInViewModel
    {
        #region Constructors

        public SignInViewModel()
        {
        }

        #endregion Constructors

        #region Data Models

        [Display(Name = "UserName")]
        [Required]
        public string UserName { get; set; }

        [Display(Name = "Password")]
        [Required]
        public string Password { get; set; }

        public bool RememberMe { get; set; }

        #endregion Data Models
    }
}