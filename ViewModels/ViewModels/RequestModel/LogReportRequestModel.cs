namespace ViewModels.ViewModels
{
    public class LogReportRequestModel : BaseSearchModel
    {
        public int? UserId { get; set; }
        public string PageName { get; set; } = String.Empty;
        public string IpAddress { get; set; } = String.Empty;
        public string? FromDate { get; set; }
        public string? ToDate { get; set; }
    }
}