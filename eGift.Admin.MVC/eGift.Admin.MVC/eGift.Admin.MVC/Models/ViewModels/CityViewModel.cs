using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.ComponentModel.DataAnnotations;

namespace eGift.Admin.MVC.Models.ViewModels
{
    public class CityViewModel : BaseModel
    {
        #region Constructors

        public CityViewModel()
        {
            StateList = new SelectList("");
            CountryList = new SelectList("");
        }

        #endregion Constructors

        #region Data Models

        public int ID { get; set; }

        [Display(Name = "City Code")]
        [Required(ErrorMessage = "This field is required.")]
        [Remote(action: "VerifyCityCode", controller: "City", AdditionalFields = nameof(ID) + "," + nameof(StateId))]
        public string CityCode { get; set; }

        [Display(Name = "City")]
        [Required(ErrorMessage = "This field is required.")]
        [Remote(action: "VerifyCityName", controller: "City", AdditionalFields = nameof(ID) + "," + nameof(StateId))]
        public string CityName { get; set; }

        [Display(Name = "State")]
        [Required(ErrorMessage = "This field is required.")]
        public int StateId { get; set; }

        [Display(Name = "Description")]
        public string? Description { get; set; }

        #endregion Data Models

        #region View Models

        [Display(Name = "Country")]
        [Required(ErrorMessage = "This field is required.")]
        public int CountryId { get; set; }

        [Display(Name = "State")]
        public string? StateName { get; set; }

        [Display(Name = "Country")]
        public string? CountryName { get; set; }

        #endregion View Models

        #region Dropdown Models

        public SelectList StateList { get; set; }

        public SelectList CountryList { get; set; }

        #endregion
    }
}