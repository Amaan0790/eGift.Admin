using eGift.Admin.MVC.Models.ViewModels;

namespace eGift.Admin.MVC.Models.ListViewModels
{
    public class OrderListViewModel
    {
        #region Constructors

        public OrderListViewModel()
        {
            OrderList = new List<OrderViewModel>();
        }

        #endregion Constructors

        #region List View Models

        public List<OrderViewModel> OrderList { get; set; }

        #endregion List View Models
    }
}