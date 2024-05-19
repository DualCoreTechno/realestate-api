namespace ViewModels.ViewModels
{
    public class PropertyModel
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public int PropertyFor { get; set; }
        public int? BhkOfficeId { get; set; }
        public string BhkOfficeName { get; set; } = String.Empty;
        public string SegmentName { get; set; } = String.Empty;
        public string PropertyTypeName { get; set; } = String.Empty;
        public int? BuildingId { get; set; }
        public string BuildingName { get; set; } = String.Empty;
        public int? AreaId { get; set; }
        public string AreaName { get; set; } = String.Empty;
        public string Address { get; set; } = String.Empty;
        public string Block { get; set; } = String.Empty;
        public string FlatNumber { get; set; } = String.Empty;
        public int? MeasurementId { get; set; }
        public string MeasurementName { get; set; } = String.Empty;
        public decimal? SuperBuiltupArea { get; set; }
        public decimal? CarpetArea { get; set; }
        public decimal? BuiltupArea { get; set; }
        public int? FurnitureStatusId { get; set; }
        public string FurnitureStatusName { get; set; } = String.Empty;
        public string Parking { get; set; } = String.Empty;
        public string? KeyStatus { get; set; }
        public decimal? PropertyPrice { get; set; }
        public string OwnerName { get; set; } = String.Empty;
        public string Mobile { get; set; } = String.Empty;
        public string Mobile1 { get; set; } = String.Empty;
        public string Mobile2 { get; set; } = String.Empty;
        public int? SourceId { get; set; }
        public string SourceName { get; set; } = String.Empty;
        public int? PropertyStatusId { get; set; }
        public string PropertyStatusName { get; set; } = String.Empty;
        public string Comission { get; set; } = String.Empty;
        public string Remark { get; set; } = String.Empty;
        public string? AvailableFrom { get; set; }
    }

    public class PropertyPageLoadModel
    {
        public List<DropDownCommonResponse> PropertyTypeList { get; set; } = new List<DropDownCommonResponse>();
        public List<DropDownCommonResponse> SegmentList { get; set; } = new List<DropDownCommonResponse>();
        public List<DropDownCommonResponse> BhkOfficeList { get; set; } = new List<DropDownCommonResponse>();
        public List<DropDownCommonResponse> BuildingList { get; set; } = new List<DropDownCommonResponse>();
        public List<DropDownCommonResponse> AreaList { get; set; } = new List<DropDownCommonResponse>();
        public List<DropDownCommonResponse> MeasurementList { get; set; } = new List<DropDownCommonResponse>();
        public List<DropDownCommonResponse> FurnitureStatusList { get; set; } = new List<DropDownCommonResponse>();
        public List<DropDownCommonResponse> PropertyStatusList { get; set; } = new List<DropDownCommonResponse>();
        public List<DropDownCommonResponse> SourceList { get; set; } = new List<DropDownCommonResponse>();
    }
}