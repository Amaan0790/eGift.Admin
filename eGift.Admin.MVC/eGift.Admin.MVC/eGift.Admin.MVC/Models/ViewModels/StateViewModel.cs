using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.ComponentModel.DataAnnotations;

namespace eGift.Admin.MVC.Models.ViewModels
{
    public class StateViewModel : BaseModel
    {
        #region Constructors

        public StateViewModel()
        {
            CountryList = new SelectList("");
        }

        #endregion Constructors

        #region Data Models

        public int ID { get; set; }

        [Display(Name = "State Code")]
        [Required(ErrorMessage = "This field is required.")]
        [Remote(action: "VerifyStateCode", controller: "State", AdditionalFields = nameof(ID) + "," + nameof(CountryId))]
        public string StateCode { get; set; }

        [Display(Name = "State")]
        [Required(ErrorMessage = "This field is required.")]
        [Remote(action: "VerifyStateName", controller: "State", AdditionalFields = nameof(ID) + "," + nameof(CountryId))]
        public string StateName { get; set; }

        [Display(Name = "Country")]
        [Required(ErrorMessage = "This field is required.")]
        public int CountryId { get; set; }

        [Display(Name = "Description")]
        public string? Description { get; set; }

        #endregion Data Models

        #region View Models

        [Display(Name = "Country")]
        public string? CountryName { get; set; }

        #endregion View Models

        #region Dropdown Models

        public SelectList CountryList { get; set; }

        #endregion
    }
}