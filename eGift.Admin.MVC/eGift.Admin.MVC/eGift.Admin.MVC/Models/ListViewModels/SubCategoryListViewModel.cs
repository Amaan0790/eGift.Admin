using eGift.Admin.MVC.Models.ViewModels;

namespace eGift.Admin.MVC.Models.ListViewModels
{
    public class SubCategoryListViewModel
    {
        #region Constructors

        public SubCategoryListViewModel()
        {
            SubCategoryList = new List<SubCategoryViewModel>();
        }

        #endregion Constructors

        #region List View Models

        public List<SubCategoryViewModel> SubCategoryList { get; set; }

        #endregion List View Models
    }
}