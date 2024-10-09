using System.ComponentModel.DataAnnotations;

namespace eGift.Admin.MVC.Models.ViewModels
{
    public class LoginViewModel
    {
        #region Constructors

        public LoginViewModel()
        {
        }

        #endregion Constructors

        #region Data Models

        public int ID { get; set; }

        [Display(Name = "Ref")]
        public int RefId { get; set; }

        [Display(Name = "RefType")]
        public string? RefType { get; set; }

        [Display(Name = "User Name")]
        [Required(ErrorMessage = "This field is required.")]
        public string UserName { get; set; }

        [Display(Name = "Password")]
        [Required(ErrorMessage = "This field is required.")]
        [RegularExpression("^(?=.*[a-z])(?=.*[A-Z])(?=.*\\d)(?=.*[@$!%*?&#])[A-Za-z\\d@$!%*?&#]{8,}$",
        ErrorMessage = "• At least one lowercase letter.<br/>• At least one uppercase letter.<br/>• At least one digit.<br/>• At least one special character.<br/>• Must be at least 8 characters long.")]
        public string Password { get; set; }

        [Display(Name = "Role")]
        public int RoleId { get; set; }

        [Display(Name = "Active")]
        public bool IsActive { get; set; } = true;

        [Display(Name = "Login Date")]
        public DateTime? LogInDate { get; set; }

        [Display(Name = "Last Login Date")]
        public DateTime? LastLogInDate { get; set; }

        #endregion Data Models

        #region View Models

        [Display(Name = "Confirm Password")]
        [Compare("Password", ErrorMessage = "Passwords do not match.")]
        public string? ConfirmPassword { get; set; }

        #endregion View Models
    }
}