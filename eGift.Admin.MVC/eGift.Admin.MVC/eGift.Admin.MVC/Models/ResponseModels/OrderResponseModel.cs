using eGift.Admin.MVC.Models.ViewModels;

namespace eGift.Admin.MVC.Models.ResponseModels
{
    public class OrderResponseModel
    {
        #region Constructors

        public OrderResponseModel()
        {
            OrderModel = new OrderViewModel();
            OrderDetailList = new List<OrderDetailsViewModel>();
        }

        #endregion

        #region Reference View Models

        public OrderViewModel OrderModel { get; set; }

        #endregion

        #region Reference List View Models

        public List<OrderDetailsViewModel> OrderDetailList { get; set; }

        #endregion
    }
}
