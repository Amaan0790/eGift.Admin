using System.ComponentModel.DataAnnotations;

namespace eGift.Admin.MVC.Models.ViewModels
{
    public class StateViewModel : BaseModel
    {
        #region Constructors

        public StateViewModel()
        {
        }

        #endregion Constructors

        #region Data Models

        public int ID { get; set; }

        [Display(Name = "StateCode")]
        [Required]
        public string StateCode { get; set; }

        [Display(Name = "State")]
        [Required]
        public string StateName { get; set; }

        [Display(Name = "Country")]
        [Required]
        public int CountryId { get; set; }

        [Display(Name = "Description")]
        public string? Description { get; set; }

        #endregion Data Models

        #region View Models

        [Display(Name = "Country")]
        public string? CountryName { get; set; }

        #endregion View Models
    }
}