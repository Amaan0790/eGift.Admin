using eGift.Admin.MVC.Models.ViewModels;

namespace eGift.Admin.MVC.Models.ListViewModels
{
    public class OrderDetailsListViewModel
    {
        #region Constructors

        public OrderDetailsListViewModel()
        {
            OrderDetailsList = new List<OrderDetailsViewModel>();
        }

        #endregion Constructors

        #region List View Models

        public List<OrderDetailsViewModel> OrderDetailsList { get; set; }

        #endregion List View Models
    }
}