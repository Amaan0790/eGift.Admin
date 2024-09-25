using eGift.Admin.MVC.Models.ViewModels;

namespace eGift.Admin.MVC.Models.ListViewModels
{
    public class CustomerListViewModel
    {
        #region Constructors

        public CustomerListViewModel()
        {
            CustomerList = new List<CustomerViewModel>();
        }

        #endregion Constructors

        #region List View Models

        public List<CustomerViewModel> CustomerList { get; set; }

        #endregion List View Models
    }
}