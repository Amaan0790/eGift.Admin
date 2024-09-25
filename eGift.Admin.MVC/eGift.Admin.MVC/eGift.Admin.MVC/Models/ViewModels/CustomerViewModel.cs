using System.ComponentModel.DataAnnotations;

namespace eGift.Admin.MVC.Models.ViewModels
{
    public class CustomerViewModel : BaseModel
    {
        #region Constructors

        public CustomerViewModel()
        {
        }

        #endregion Constructors

        #region Data Models

        public int ID { get; set; }

        [Display(Name = "FirstName")]
        [Required]
        public string FirstName { get; set; }

        [Display(Name = "LastName")]
        [Required]
        public string LastName { get; set; }

        [Display(Name = "DateOfBirth")]
        [Required]
        public DateTime DateOfBirth { get; set; }

        [Display(Name = "Gender")]
        [Required]
        public int GenderId { get; set; }

        [Display(Name = "Mobile")]
        [Required]
        public string Mobile { get; set; }

        [Display(Name = "Email")]
        public string? Email { get; set; }

        [Display(Name = "Address")]
        public int? AddressId { get; set; }

        [Display(Name = "IsActive")]
        [Required]
        public bool IsActive { get; set; } = true;

        [Display(Name = "ProfileImagePath")]
        public string? ProfileImagePath { get; set; }

        [Display(Name = "ProfileImageData")]
        public byte[]? ProfileImageData { get; set; }

        [Display(Name = "Role")]
        [Required]
        public int RoleId { get; set; }

        [Display(Name = "IsDefault")]
        [Required]
        public bool IsDefault { get; set; } = false;

        #endregion Data Models

        #region View Models

        [Display(Name = "Age")]
        public int? Age { get; set; }

        [Display(Name = "Address")]
        public string? AddressName { get; set; }

        [Display(Name = "Gender")]
        public string? GenderName { get; set; }

        [Display(Name = "Quantity")]
        public int? Quantity { get; set; }

        [Display(Name = "Role")]
        public string? RoleName { get; set; }

        [Display(Name = "ProfileImage")]
        public IFormFile? ProfileImage { get; set; }

        #endregion View Models
    }
}