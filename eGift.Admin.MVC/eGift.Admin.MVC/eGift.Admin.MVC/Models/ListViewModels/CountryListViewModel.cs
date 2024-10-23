using eGift.Admin.MVC.Models.ViewModels;

namespace eGift.Admin.MVC.Models.ListViewModels
{
    public class CountryListViewModel
    {
        #region Constructors

        public CountryListViewModel()
        {
            CountryList = new List<CountryViewModel>();
            CountryModel = new CountryViewModel();
        }

        #endregion Constructors

        #region List View Models

        public List<CountryViewModel> CountryList { get; set; }

        #endregion List View Models

        #region Reference View Models

        public CountryViewModel CountryModel { get; set; }

        #endregion
    }
}