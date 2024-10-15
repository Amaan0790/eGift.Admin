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

        [Display(Name = "User Name")]
        [Required(ErrorMessage = "This field is required.")]
        public string UserName { get; set; }

        [Display(Name = "Password")]
        [Required(ErrorMessage = "This field is required.")]
        [DataType(DataType.Password)]
        public string Password { get; set; }

        [Display(Name = "Remember Me")]
        public bool RememberMe { get; set; }

        #endregion Data Models
    }
}