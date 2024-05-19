namespace ViewModels.ViewModels
{
    public class SegmentModel : MasterCommonFieldsModel
    {
        public int PropertyTypeId { get; set; }
        public string PropertyTypeName { get; set; } = string.Empty;
    }

    public class SegmentResponseModel
    {
        public List<DropDownCommonResponse> PropertyTypeList { get; set; } = new List<DropDownCommonResponse>();
    }
}
