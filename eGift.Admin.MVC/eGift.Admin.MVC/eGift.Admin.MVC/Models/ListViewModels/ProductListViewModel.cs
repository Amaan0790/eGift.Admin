using eGift.Admin.MVC.Models.ViewModels;

namespace eGift.Admin.MVC.Models.ListViewModels
{
    public class ProductListViewModel
    {
        #region Constructors

        public ProductListViewModel()
        {
            ProductList = new List<ProductViewModel>();
            ProductModel = new ProductViewModel();
        }

        #endregion Constructors

        #region List View Models

        public List<ProductViewModel> ProductList { get; set; }

        #endregion List View Models

        #region Reference View Models

        public ProductViewModel ProductModel { get; set; }

        #endregion
    }
}