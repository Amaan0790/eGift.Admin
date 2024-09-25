using eGift.Admin.MVC.Models.ViewModels;

namespace eGift.Admin.MVC.Models.ListViewModels
{
    public class GenderListViewModel
    {
        #region Constructors

        public GenderListViewModel()
        {
            GenderList = new List<GenderViewModel>();
        }

        #endregion Constructors

        #region List View Models

        public List<GenderViewModel> GenderList { get; set; }

        #endregion List View Models
    }
}