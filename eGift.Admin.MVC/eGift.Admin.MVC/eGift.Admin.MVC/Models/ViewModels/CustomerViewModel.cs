using eGift.Admin.MVC.Common;
using eGift.Admin.MVC.Helpers;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.ComponentModel.DataAnnotations;

namespace eGift.Admin.MVC.Models.ViewModels
{
    public class CustomerViewModel : BaseModel
    {
        #region Constructors

        public CustomerViewModel()
        {
            GenderList = EnumHelper.EnumNameToSelectList<Gender>();
            LoginModel = new LoginViewModel();
        }

        #endregion Constructors

        #region Data Models

        public int ID { get; set; }

        [Display(Name = "First Name")]
        [Required(ErrorMessage = "This field is required.")]
        public string FirstName { get; set; }

        [Display(Name = "Last Name")]
        [Required(ErrorMessage = "This field is required.")]
        public string LastName { get; set; }

        [Display(Name = "Date Of Birth")]
        [Required(ErrorMessage = "This field is required.")]
        [DataType(DataType.Date)]
        public DateTime? DateOfBirth { get; set; }

        [Display(Name = "Gender")]
        [Required(ErrorMessage = "This field is required.")]
        public int GenderId { get; set; }

        [Display(Name = "Mobile")]
        [Required(ErrorMessage = "This field is required.")]
        public string Mobile { get; set; }

        [Display(Name = "Email")]
        public string? Email { get; set; }

        [Display(Name = "Address")]
        public int? AddressId { get; set; }

        [Display(Name = "Active")]
        [Required(ErrorMessage = "This field is required.")]
        public bool IsActive { get; set; } = true;

        [Display(Name = "Profile Image")]
        public string? ProfileImagePath { get; set; }

        [Display(Name = "Profile Image")]
        public byte[]? ProfileImageData { get; set; }

        [Display(Name = "Role")]
        public int RoleId { get; set; }

        [Display(Name = "IsDefault")]
        public bool IsDefault { get; set; } = false;

        #endregion Data Models

        #region View Models

        [Display(Name = "Age")]
        public int? Age { get; set; }

        [Display(Name = "Address")]
        public string? AddressName { get; set; }

        [Display(Name = "Gender")]
        public string? GenderName { get; set; }

        [Display(Name = "Role")]
        public string? RoleName { get; set; }

        [Display(Name = "Profile Image")]
        public IFormFile? ProfileImage { get; set; }

        public bool IsClear { get; set; }

        [Display(Name = "User Name")]
        [Required(ErrorMessage = "This field is required.")]
        [Remote(action: "VerifyUserName", controller: "Customer", AdditionalFields = nameof(ID))]
        public string UserName { get; set; }

        #endregion View Models

        #region Reference View Models

        public LoginViewModel LoginModel { get; set; }

        #endregion

        #region Dropdown Lists

        public SelectList GenderList { get; set; }

        #endregion
    }
}