using System.ComponentModel.DataAnnotations;

namespace eGift.Admin.MVC.Models.ViewModels
{
    public class CityViewModel : BaseModel
    {
        #region Constructors

        public CityViewModel()
        {
        }

        #endregion Constructors

        #region Data Models

        public int ID { get; set; }

        [Display(Name = "CityCode")]
        [Required]
        public string CityCode { get; set; }

        [Display(Name = "CityName")]
        [Required]
        public string CityName { get; set; }

        [Display(Name = "State")]
        [Required]
        public int StateId { get; set; }

        [Display(Name = "Description")]
        public string? Description { get; set; }

        #endregion Data Models

        #region View Models

        [Display(Name = "StateName")]
        public string? StateName { get; set; }

        #endregion View Models
    }
}