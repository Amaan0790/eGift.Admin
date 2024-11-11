using eGift.Admin.MVC.Common;
using eGift.Admin.MVC.Helpers;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.ComponentModel.DataAnnotations;

namespace eGift.Admin.MVC.Models.ViewModels
{
    public class ProductViewModel : BaseModel
    {
        #region Constructors

        public ProductViewModel()
        {
            CategoryList = new SelectList("");
            SubCategoryList = new SelectList("");
            SizeList = EnumHelper.EnumDescriptionToSelectList<Size>();
        }

        #endregion Constructors

        #region Data Models

        public int ID { get; set; }

        [Display(Name = "Name")]
        [Required(ErrorMessage = "This field is required.")]
        [Remote(action: "VerifyProductName", controller: "Product", AdditionalFields = nameof(ID) + "," + nameof(CategoryId) + "," + nameof(SubCategoryId))]
        public string Name { get; set; }

        [Display(Name = "Category")]
        [Required(ErrorMessage = "This field is required.")]
        public int CategoryId { get; set; }

        [Display(Name = "Sub Category")]
        [Required(ErrorMessage = "This field is required.")]
        public int SubCategoryId { get; set; }

        [Display(Name = "Quantity Per Unit")]
        public int? QuantityPerUnit { get; set; }

        [Display(Name = "Unit Price")]
        [Required(ErrorMessage = "This field is required.")]
        public decimal? UnitPrice { get; set; }

        [Display(Name = "Size")]
        public int? SizeId { get; set; }

        [Display(Name = "Discount")]
        public decimal? Discount { get; set; }

        [Display(Name = "Unit In Stock")]
        public int? UnitInStock { get; set; }

        [Display(Name = "Unit In Order")]
        public int? UnitInOrder { get; set; }

        [Display(Name = "Product Available")]
        public int? ProductAvailable { get; set; }

        [Display(Name = "Short Description")]
        public string? ShortDescription { get; set; }

        [Display(Name = "Long Description")]
        public string? LongDescription { get; set; }

        [Display(Name = "Picture Path 1")]
        public string? PicturePath1 { get; set; }

        [Display(Name = "Picture Path 2")]
        public string? PicturePath2 { get; set; }

        [Display(Name = "Picture Path 3")]
        public string? PicturePath3 { get; set; }

        [Display(Name = "Picture Path 4")]
        public string? PicturePath4 { get; set; }

        [Display(Name = "Picture Data 1")]
        public byte[]? PictureData1 { get; set; }

        [Display(Name = "Picture Data 2")]
        public byte[]? PictureData2 { get; set; }

        [Display(Name = "Picture Data 3")]
        public byte[]? PictureData3 { get; set; }

        [Display(Name = "Picture Data 4")]
        public byte[]? PictureData4 { get; set; }

        [Display(Name = "Product Image")]
        public string? ProductImagePath { get; set; }

        [Display(Name = "Product Image")]
        public byte[]? ProductImageData { get; set; }

        #endregion Data Models

        #region View Models

        [Display(Name = "Image Url")]
        public IFormFile? ImageUrl { get; set; }

        [Display(Name = "Picture 1")]
        public IFormFile? Picture1 { get; set; }

        [Display(Name = "Picture 2")]
        public IFormFile? Picture2 { get; set; }

        [Display(Name = "Picture 3")]
        public IFormFile? Picture3 { get; set; }

        [Display(Name = "Picture 4")]
        public IFormFile? Picture4 { get; set; }

        [Display(Name = "Category")]
        public string? CategoryName { get; set; }

        [Display(Name = "Sub Category")]
        public string? SubCategoryName { get; set; }

        [Display(Name = "Size")]
        public string? SizeName { get; set; }

        public bool IsClear { get; set; }

        #endregion View Models

        #region Dropdown Models

        public SelectList CategoryList { get; set; }

        public SelectList SubCategoryList { get; set; }

        public SelectList SizeList { get; set; }

        #endregion
    }
}