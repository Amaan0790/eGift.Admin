using System.ComponentModel.DataAnnotations;

namespace eGift.Admin.MVC.Models.ViewModels
{
    public class SubCategoryViewModel : BaseModel
    {
        #region Constructors

        public SubCategoryViewModel()
        {
        }

        #endregion Constructors

        #region Data Models

        public int ID { get; set; }

        [Display(Name = "Category")]
        [Required]
        public int CategoryId { get; set; }

        [Display(Name = "SubCategory")]
        [Required]
        public string SubCategoryName { get; set; }

        [Display(Name = "Description")]
        public string? Description { get; set; }

        #endregion Data Models

        #region View Models

        [Display(Name = "Category")]
        public string? CategoryName { get; set; }

        #endregion View Models
    }
}