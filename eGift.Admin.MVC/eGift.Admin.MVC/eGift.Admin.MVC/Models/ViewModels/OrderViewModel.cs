using System.ComponentModel.DataAnnotations;

namespace eGift.Admin.MVC.Models.ViewModels
{
    public class OrderViewModel : BaseModel
    {
        #region Constructors

        public OrderViewModel()
        {
        }

        #endregion Constructors

        #region Data Models

        public int ID { get; set; }

        [Display(Name = "Customer")]
        [Required]
        public int CustomerId { get; set; }

        [Display(Name = "TotalAmount")]
        [Required]
        public decimal TotalAmount { get; set; }

        [Display(Name = "TotalDiscount")]
        public decimal? TotalDiscount { get; set; }

        [Display(Name = "TotalTax")]
        public decimal? TotalTax { get; set; }

        [Display(Name = "OrderNumber")]
        [Required]
        public string OrderNumber { get; set; }

        [Display(Name = "Notes")]
        public string? Notes { get; set; }

        [Display(Name = "DispatchedDate")]
        public DateTime? DispatchedDate { get; set; }

        [Display(Name = "ShippedDate")]
        public DateTime? ShippedDate { get; set; }

        [Display(Name = "DeliveryDate")]
        public DateTime? DeliveryDate { get; set; }

        [Display(Name = "CancelDate")]
        public DateTime? CancelDate { get; set; }

        [Display(Name = "Status")]
        [Required]
        public int StatusId { get; set; }

        #endregion Data Models

        #region View Models

        [Display(Name = "Status")]
        public string? StatusName { get; set; }

        [Display(Name = "CustomerName")]
        public string? CustomerName { get; set; }

        #endregion View Models
    }
}