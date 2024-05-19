namespace ViewModels.ViewModels
{
    public class BhkOfficeModel : MasterCommonFieldsModel
    {
        public int SegmentId { get; set; }
        public string SegmentName { get; set; } = String.Empty;
    }

    public class BhkOfficeResponseModel
    {
        public List<DropDownCommonResponse> SegmentList { get; set; } = new List<DropDownCommonResponse>();
    }
}
