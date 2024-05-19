namespace ViewModels.ViewModels
{
    public class LogReportModel
    {
        public string Description { get; set; } = String.Empty;
        public string PageName { get; set; } = String.Empty;
        public string UserName { get; set; } = String.Empty;
        public DateTime CreatedOn { get; set; }
        public string IpAddress { get; set; } = String.Empty;
    }

    public class ReportPageLoadModel
    {
        public List<DropDownCommonResponse> UserList { get; set; } = new List<DropDownCommonResponse>();
        public List<string?> PageList { get; set; } = new List<string?>();
    }
}
