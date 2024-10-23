using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;

namespace eGift.Admin.MVC.Models.ViewModels
{
    public class CountryViewModel : BaseModel
    {
        #region Constructors

        public CountryViewModel()
        {
        }

        #endregion Constructors

        #region Data Models

        public int ID { get; set; }

        [Display(Name = "Country Code")]
        [Required(ErrorMessage = "This field is required.")]
        [Remote(action: "VerifyCountryCode", controller: "Country", AdditionalFields = nameof(ID))]
        public string CountryCode { get; set; }

        [Display(Name = "Country")]
        [Required(ErrorMessage = "This field is required.")]
        [Remote(action: "VerifyCountryName", controller: "Country", AdditionalFields = nameof(ID))]
        public string CountryName { get; set; }

        [Display(Name = "Description")]
        public string? Description { get; set; }

        #endregion Data Models
    }
}