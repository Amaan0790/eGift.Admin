using System.ComponentModel.DataAnnotations;

namespace eGift.Admin.MVC.Models.ViewModels
{
    public class CategoryViewModel : BaseModel
    {
        #region Constructors

        public CategoryViewModel()
        {
        }

        #endregion Constructors

        #region Data Models

        public int ID { get; set; }

        [Display(Name = "Category")]
        [Required]
        public string CategoryName { get; set; }

        [Display(Name = "Description")]
        public string? Description { get; set; }

        #endregion Data Models
    }
}