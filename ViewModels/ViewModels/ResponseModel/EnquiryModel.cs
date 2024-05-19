namespace ViewModels.ViewModels
{
    public class EnquiryModel
    {
        public int Id { get; set; }
        public string NameOfClient { get; set; } = String.Empty;
        public string MobileNo { get; set; } = String.Empty;
        public string Email { get; set; } = String.Empty;
        public int? EnquiryFor { get; set; }
        public int? SourceId { get; set; }
        public string SourceName { get; set; } = String.Empty;
        public int? BhkOfficeId { get; set; }
        public string BhkOfficeName { get; set; } = String.Empty;
        public string SegmentName { get; set; } = String.Empty;
        public string PropertyTypeName { get; set; } = String.Empty;
        public int? EnquiryStatusId { get; set; }
        public string EnquiryStatusName { get; set; } = String.Empty;
        public int? BudgetId { get; set; }
        public string BudgetName { get; set; } = String.Empty;
        public int? NonuseId { get; set; }
        public string NonuseName { get; set; } = String.Empty;
        public string AreaId { get; set; } = String.Empty;
        public string AreaName { get; set; } = String.Empty;
        public string Remark { get; set; } = String.Empty;
        public string LastRemark { get; set; } = String.Empty;
        public int AssignTo { get; set; }
        public string AssignToName { get; set; } = String.Empty;
        public int AssignBy { get; set; }
        public string AssignByName { get; set; } = String.Empty;
        public string Nfd { get; set; } = String.Empty;
        //public bool? IsClosed { get; set; }
        public string Mobile1 { get; set; } = String.Empty;
        public string Mobile2 { get; set; } = String.Empty;
        public string Mobile3 { get; set; } = String.Empty;
        public string CreatedOn { get; set; } = String.Empty;
    }

    public class EnquiryResponseModel
    {
        public int TodaysCount { get; set; }
        public int TomorrowsCount { get; set; }
        public int PendingCount { get; set; }
        public List<EnquiryModel>? EnquiryList { get; set; }
    }

    public class EnquiryRemarksModel
    {
        public int EnquiryId { get; set; }
        public int? ActivityChildId { get; set; }
        public string ActivityChildName { get; set; } = String.Empty;
        public string Nfd { get; set; } = String.Empty;
        public int EnquiryStatusId { get; set; }
        public string EnquiryStatusName { get; set; } = String.Empty;
        public string Remark { get; set; } = String.Empty;
        //public bool? IsClosed { get; set; }
        public int CreatedBy { get; set; }
        public string CreatedByName { get; set; } = String.Empty;
        public string CreatedOn { get; set; } = String.Empty;
    }

    public class EnquiryPageLoadModel
    {
        public List<DropDownCommonResponse> SourceList { get; set; } = new List<DropDownCommonResponse>();
        public List<DropDownCommonResponse> BhkOfficeList { get; set; } = new List<DropDownCommonResponse>();
        public List<BudgetDropdownModel> BudgetList { get; set; } = new List<BudgetDropdownModel>();
        public List<DropDownCommonResponse> NonuseList { get; set; } = new List<DropDownCommonResponse>();
        public List<DropDownCommonResponse> AreaList { get; set; } = new List<DropDownCommonResponse>();
        public List<DropDownCommonResponse> EnquiryStatusList { get; set; } = new List<DropDownCommonResponse>();
        public List<DropDownCommonResponse> ActivityParentList { get; set; } = new List<DropDownCommonResponse>();
        public List<ActivityDropDownResponse> ActivityChildList { get; set; } = new List<ActivityDropDownResponse>();
        public List<DropDownCommonResponse> UserList { get; set; } = new List<DropDownCommonResponse>();
    }

    public class EnquiryCheckMobileModel
    {
        public int Id { get; set; }
        public string MobileNo { get; set; } = String.Empty;
    }
}