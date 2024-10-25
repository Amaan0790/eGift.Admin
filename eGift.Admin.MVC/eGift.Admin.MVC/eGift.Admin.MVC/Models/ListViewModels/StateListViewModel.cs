using eGift.Admin.MVC.Models.ViewModels;

namespace eGift.Admin.MVC.Models.ListViewModels
{
    public class StateListViewModel
    {
        #region Constructors

        public StateListViewModel()
        {
            StateList = new List<StateViewModel>();
            StateModel = new StateViewModel();
        }

        #endregion Constructors

        #region List View Models

        public List<StateViewModel> StateList { get; set; }

        #endregion List View Models

        #region Reference View Models
        public StateViewModel StateModel { get; set; }
        #endregion
    }
}