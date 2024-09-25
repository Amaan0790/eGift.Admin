using System.ComponentModel.DataAnnotations;

namespace eGift.Admin.MVC.Models.ViewModels
{
    public class AddressViewModel : BaseModel
    {
        #region Constructors

        public AddressViewModel()
        {
        }

        #endregion Constructors

        #region Data Models

        public int ID { get; set; }

        [Display(Name = "Street1")]
        [Required(ErrorMessage = "This field is required.")]
        public string Street1 { get; set; }

        [Display(Name = "Street2")]
        public string? Street2 { get; set; }

        [Display(Name = "Country")]
        [Required(ErrorMessage = "This field is required.")]
        public int CountryId { get; set; }

        [Display(Name = "State")]
        [Required(ErrorMessage = "This field is required.")]
        public int StateId { get; set; }

        [Display(Name = "City")]
        [Required(ErrorMessage = "This field is required.")]
        public int CityId { get; set; }

        [Display(Name = "Pincode")]
        public string? Pincode { get; set; }

        #endregion Data Models

        #region View Models

        [Display(Name = "Country")]
        public string? CountryName { get; set; }

        [Display(Name = "State")]
        public string? StateName { get; set; }

        [Display(Name = "City")]
        public string? CityName { get; set; }

        #endregion View Models
    }
}