using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.ComponentModel.DataAnnotations;

namespace eGift.Admin.MVC.Models.ViewModels
{
    public class SubCategoryViewModel : BaseModel
    {
        #region Constructors

        public SubCategoryViewModel()
        {
            CategoryList = new SelectList("");
        }

        #endregion Constructors

        #region Data Models

        public int ID { get; set; }

        [Display(Name = "Category")]
        [Required(ErrorMessage = "This field is required.")]
        public int CategoryId { get; set; }

        [Display(Name = "Sub Category")]
        [Required(ErrorMessage = "This field is required.")]
        [Remote(action: "VerifySubCategoryName", controller: "SubCategory", AdditionalFields = nameof(ID) + "," + nameof(CategoryId))]
        public string SubCategoryName { get; set; }

        [Display(Name = "Description")]
        public string? Description { get; set; }

        #endregion Data Models

        #region View Models

        [Display(Name = "Category")]
        public string? CategoryName { get; set; }

        #endregion View Models

        #region Dropdown Models

        public SelectList CategoryList { get; set; }

        #endregion
    }
}