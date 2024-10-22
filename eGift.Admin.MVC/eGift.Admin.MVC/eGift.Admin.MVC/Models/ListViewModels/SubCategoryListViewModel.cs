using eGift.Admin.MVC.Models.ViewModels;

namespace eGift.Admin.MVC.Models.ListViewModels
{
    public class SubCategoryListViewModel
    {
        #region Constructors

        public SubCategoryListViewModel()
        {
            SubCategoryList = new List<SubCategoryViewModel>();
            SubCategoryModel = new SubCategoryViewModel();
        }

        #endregion Constructors

        #region List View Models

        public List<SubCategoryViewModel> SubCategoryList { get; set; }

        #endregion List View Models

        #region Reference View Models

        public SubCategoryViewModel SubCategoryModel { get; set; }

        #endregion
    }
}