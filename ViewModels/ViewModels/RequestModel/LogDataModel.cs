using ViewModels.Enums;

namespace ViewModels.ViewModels
{
    public class LogDataModel
    {
        public int UserId { get; set; }
        public string Description { get; set; } = String.Empty;
        public LogTypeEnum Module { get; set; }
        public string? PageName { get; set; } = String.Empty;
        public string? IpAddress { get; set; }
    }
}
