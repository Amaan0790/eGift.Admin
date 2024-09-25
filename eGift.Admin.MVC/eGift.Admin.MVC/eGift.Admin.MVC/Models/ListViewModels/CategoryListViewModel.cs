using eGift.Admin.MVC.Models.ViewModels;

namespace eGift.Admin.MVC.Models.ListViewModels
{
    public class CategoryListViewModel
    {
        #region Constructors

        public CategoryListViewModel()
        {
            CategoryList = new List<CategoryViewModel>();
        }

        #endregion Constructors

        #region List View Models

        public List<CategoryViewModel> CategoryList { get; set; }

        #endregion List View Models
    }
}