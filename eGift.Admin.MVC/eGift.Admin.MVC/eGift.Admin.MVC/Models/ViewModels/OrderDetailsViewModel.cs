using System.ComponentModel.DataAnnotations;

namespace eGift.Admin.MVC.Models.ViewModels
{
    public class OrderDetailsViewModel : BaseModel
    {
        #region Constructors

        public OrderDetailsViewModel()
        {
        }

        #endregion Constructors

        #region Data Models

        public int ID { get; set; }

        [Display(Name = "Order")]
        [Required]
        public int OrderId { get; set; }

        [Display(Name = "Product")]
        [Required]
        public int ProductId { get; set; }

        [Display(Name = "UnitPrice")]
        [Required]
        public decimal UnitPrice { get; set; }

        [Display(Name = "Quantity")]
        [Required]
        public int Quantity { get; set; }

        [Display(Name = "Discount")]
        public decimal? Discount { get; set; }

        [Display(Name = "Tax")]
        public decimal? Tax { get; set; }

        [Display(Name = "NetAmount")]
        [Required]
        public decimal NetAmount { get; set; }

        #endregion Data Models
    }
}