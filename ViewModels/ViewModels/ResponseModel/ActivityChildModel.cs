namespace ViewModels.ViewModels
{
    public class ActivityChildModel : MasterCommonFieldsModel
    {
        public int ActivityParentId { get; set; }
        public string ActivityParentName { get; set; } = String.Empty;
    }

    public class ActivityDropDownResponse : DropDownCommonResponse
    {
        public int ActivityParentId { get; set; }

    }

    public class ActivityChildResponseModel
    {
        public List<DropDownCommonResponse> ActivityParentList { get; set; } = new List<DropDownCommonResponse>();
    }
}
