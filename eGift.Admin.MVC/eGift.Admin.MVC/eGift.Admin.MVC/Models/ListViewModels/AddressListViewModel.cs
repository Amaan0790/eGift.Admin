using eGift.Admin.MVC.Models.ViewModels;

namespace eGift.Admin.MVC.Models.ListViewModels
{
    public class AddressListViewModel
    {
        #region Constructors

        public AddressListViewModel()
        {
            AddressList = new List<AddressViewModel>();
            AddressModel = new AddressViewModel();
        }

        #endregion Constructors

        #region List View Models

        public List<AddressViewModel> AddressList { get; set; }

        #endregion List View Models

        #region Reference View Models

        public AddressViewModel AddressModel { get; set; }

        #endregion
    }
}