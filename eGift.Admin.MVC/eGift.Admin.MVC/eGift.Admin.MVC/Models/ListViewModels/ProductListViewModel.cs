using eGift.Admin.MVC.Models.ViewModels;

namespace eGift.Admin.MVC.Models.ListViewModels
{
    public class ProductListViewModel
    {
        #region Constructors

        public ProductListViewModel()
        {
            ProductList = new List<ProductViewModel>();
        }

        #endregion Constructors

        #region List View Models

        public List<ProductViewModel> ProductList { get; set; }

        #endregion List View Models
    }
}