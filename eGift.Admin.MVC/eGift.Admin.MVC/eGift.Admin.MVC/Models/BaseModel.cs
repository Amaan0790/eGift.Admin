namespace eGift.Admin.MVC.Models
{
    public class BaseModel
    {
        #region Data Models

        public bool IsDeleted { get; set; }
        public int CreatedBy { get; set; }
        public DateTime CreatedDate { get; set; }
        public int? UpdatedBy { get; set; }
        public DateTime? UpdatedDate { get; set; }

        #endregion Data Models
    }
}