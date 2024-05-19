namespace ViewModels.ViewModels
{
    public class LoginReportModel
    {
        public string UserName { get; set; } = String.Empty;
        public DateTime? LastLoginTime { get; set; }
        public string LastLoginIp { get; set; } = String.Empty;
        public bool LoginToday { get; set; }
    }
}