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
        [Required]
        public int RefId { get; set; }

        [Display(Name = "RefType")]
        [Required]
        public string RefType { get; set; }

        [Display(Name = "UserName")]
        [Required]
        public string UserName { get; set; }

        [Display(Name = "Password")]
        [Required]
        public string Password { get; set; }

        [Display(Name = "Role")]
        [Required]
        public int RoleId { get; set; }

        [Display(Name = "IsActive")]
        [Required]
        public bool IsActive { get; set; } = true;

        [Display(Name = "LogInDate")]
        public DateTime? LogInDate { get; set; }

        [Display(Name = "LastLogInDate")]
        public DateTime? LastLogInDate { get; set; }

        #endregion Data Models

        #region View Models

        [Display(Name = "ConfirmPassword")]
        public string? ConfirmPassword { get; set; }

        #endregion View Models
    }
}