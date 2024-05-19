namespace ViewModels.ViewModels
{
    public class BudgetModel : MasterCommonFieldsModel
    {
        public int ForValue { get; set; }
        public int BhkOfficeId { get; set; }
        public string BhkOfficeName { get; set; } = String.Empty;
        public int From { get; set; }
        public int To { get; set; }
    }
    
    public class BudgetResponseModel
    {
        public List<DropDownCommonResponse> BhkOfficeList { get; set; } = new List<DropDownCommonResponse>();
    }

    public class BudgetDropdownModel : DropDownCommonResponse
    {
        public int For { get; set; }
        public int BhkOfficeId { get; set; }
    }
}
