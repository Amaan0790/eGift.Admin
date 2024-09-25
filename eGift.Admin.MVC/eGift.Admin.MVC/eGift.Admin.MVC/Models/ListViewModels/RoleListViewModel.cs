using eGift.Admin.MVC.Models.ViewModels;

namespace eGift.Admin.MVC.Models.ListViewModels
{
    public class RoleListViewModel
    {
        #region Constructors

        public RoleListViewModel()
        {
            RoleList = new List<RoleViewModel>();
        }

        #endregion Constructors

        #region List View Models

        public List<RoleViewModel> RoleList { get; set; }

        #endregion List View Models
    }
}