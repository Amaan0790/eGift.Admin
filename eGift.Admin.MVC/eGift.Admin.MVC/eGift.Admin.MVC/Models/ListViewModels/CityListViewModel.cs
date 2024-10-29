using eGift.Admin.MVC.Models.ViewModels;

namespace eGift.Admin.MVC.Models.ListViewModels
{
    public class CityListViewModel
    {
        #region Constructors

        public CityListViewModel()
        {
            CityList = new List<CityViewModel>();
            CityModel = new CityViewModel();
        }

        #endregion Constructors

        #region List View Models

        public List<CityViewModel> CityList { get; set; }

        #endregion List View Models

        #region Reference View Models

        public CityViewModel CityModel { get; set; }

        #endregion
    }
}