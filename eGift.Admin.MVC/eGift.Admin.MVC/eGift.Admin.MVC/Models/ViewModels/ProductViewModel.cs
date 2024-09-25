using System.ComponentModel.DataAnnotations;

namespace eGift.Admin.MVC.Models.ViewModels
{
    public class ProductViewModel : BaseModel
    {
        #region Constructors

        public ProductViewModel()
        {
        }

        #endregion Constructors

        #region Data Models

        public int ID { get; set; }

        [Display(Name = "Name")]
        [Required]
        public string Name { get; set; }

        [Display(Name = "Category")]
        [Required]
        public int CategoryId { get; set; }

        [Display(Name = "SubCategory")]
        [Required]
        public int SubCategoryId { get; set; }

        [Display(Name = "QuantityPerPrice")]
        public int? QuantityPerPrice { get; set; }

        [Display(Name = "UnitPrice")]
        [Required]
        public decimal UnitPrice { get; set; }

        [Display(Name = "Size")]
        public int? SizeId { get; set; }

        [Display(Name = "Discount")]
        public decimal? Discount { get; set; }

        [Display(Name = "UnitInStock")]
        public int? UnitInStock { get; set; }

        [Display(Name = "UnitInOrder")]
        public int? UnitInOrder { get; set; }

        [Display(Name = "ProductAvailable")]
        public int? ProductAvailable { get; set; }

        [Display(Name = "ShortDescription")]
        public string? ShortDescription { get; set; }

        [Display(Name = "LongDescription")]
        public string? LongDescription { get; set; }

        [Display(Name = "PicturePath1")]
        public string? PicturePath1 { get; set; }

        [Display(Name = "PicturePath2")]
        public string? PicturePath2 { get; set; }

        [Display(Name = "PicturePath3")]
        public string? PicturePath3 { get; set; }

        [Display(Name = "PicturePath4")]
        public string? PicturePath4 { get; set; }

        [Display(Name = "PictureData1")]
        public byte[]? PictureData1 { get; set; }

        [Display(Name = "PictureData2")]
        public byte[]? PictureData2 { get; set; }

        [Display(Name = "PictureData3")]
        public byte[]? PictureData3 { get; set; }

        [Display(Name = "PictureData4")]
        public byte[]? PictureData4 { get; set; }

        [Display(Name = "ProductImagePath")]
        [Required]
        public string ProductImagePath { get; set; }

        [Display(Name = "ProductImageData")]
        [Required]
        public byte[] ProductImageData { get; set; }

        #endregion Data Models

        #region View Models

        [Display(Name = "ImageUrl")]
        public IFormFile? ImageUrl { get; set; }

        [Display(Name = "Picture1")]
        public IFormFile? Picture1 { get; set; }

        [Display(Name = "Picture2")]
        public IFormFile? Picture2 { get; set; }

        [Display(Name = "Picture3")]
        public IFormFile? Picture3 { get; set; }

        [Display(Name = "Picture4")]
        public IFormFile? Picture4 { get; set; }

        [Display(Name = "Category")]
        public string? CategoryName { get; set; }

        [Display(Name = "SubCategory")]
        public string? SubCategoryName { get; set; }

        [Display(Name = "Size")]
        public string? SizeName { get; set; }

        #endregion View Models
    }
}