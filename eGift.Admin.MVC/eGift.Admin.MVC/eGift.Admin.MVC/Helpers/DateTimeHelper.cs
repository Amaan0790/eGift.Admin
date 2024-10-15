namespace eGift.Admin.MVC.Helpers
{
    public static class DateTimeHelper
    {
        #region Datetime To Date String

        public static string ToDateString(this DateTime dateTime)
        {
            return dateTime.ToString("yyyy-MM-dd");
        }

        public static string ToDateString(this DateTime? dateTime)
        {
            return dateTime?.ToString("yyyy-MM-dd");
        }

        #endregion

        #region Datetime To Date and Time String

        public static string ToDateTimeString(this DateTime dateTime)
        {
            return dateTime.ToString("yyyy-MM-dd hh:mm tt");
        }

        public static string ToDateTimeString(this DateTime? dateTime)
        {
            return dateTime?.ToString("yyyy-MM-dd hh:mm tt");
        }

        #endregion
    }
}
