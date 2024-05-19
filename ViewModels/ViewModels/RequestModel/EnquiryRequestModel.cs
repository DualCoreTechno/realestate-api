namespace ViewModels.ViewModels
{
    public class EnquiryRequestModel : BaseSearchModel
    {
        public int? EnquiryFor { get; set; }
        public int? BhkOfficeId { get; set; }
        public int? BudgetId { get; set; }
        public int? SourceId { get; set; }
        public int? EnquiryStatusId { get; set; }
        public int? NonuseId { get; set; }
        public int? EmployeeId { get; set; }
        public string? FromNfd { get; set; }
        public string? ToNfd { get; set; }
    }
}