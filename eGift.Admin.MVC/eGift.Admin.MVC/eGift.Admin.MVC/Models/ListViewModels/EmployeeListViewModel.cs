using eGift.Admin.MVC.Models.ViewModels;

namespace eGift.Admin.MVC.Models.ListViewModels
{
    public class EmployeeListViewModel
    {
        #region Constructors

        public EmployeeListViewModel()
        {
            EmployeeList = new List<EmployeeViewModel>();
        }

        #endregion Constructors

        #region List View Models

        public List<EmployeeViewModel> EmployeeList { get; set; }

        #endregion List View Models
    }
}