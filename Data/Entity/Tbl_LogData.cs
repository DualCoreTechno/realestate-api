using ViewModels.Enums;

namespace Data.Entity
{
    public class Tbl_LogData : BaseEntity
    {
        public string Description { get; set; } = String.Empty;
        public LogTypeEnum Module { get; set; }
        public string? PageName { get; set; } = String.Empty;
        public string? IpAddress { get; set; }
    }
}
