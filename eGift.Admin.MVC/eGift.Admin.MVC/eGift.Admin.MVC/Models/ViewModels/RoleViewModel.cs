using System.ComponentModel.DataAnnotations;

namespace eGift.Admin.MVC.Models.ViewModels
{
    public class RoleViewModel : BaseModel
    {
        #region Constructors

        public RoleViewModel()
        {
        }

        #endregion Constructors

        #region Data Models

        public int ID { get; set; }

        [Display(Name = "Role")]
        [Required]
        public string RoleName { get; set; }

        [Display(Name = "Description")]
        public string? Description { get; set; }

        #endregion Data Models
    }
}