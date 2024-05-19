namespace ViewModels.ViewModels
{
    public class PropertyRequestModel : BaseSearchModel
    {
        public int? PropertyFor { get; set; }
        public int? BhkOfficeId { get; set; }
        public int? BuildingId { get; set; }
        public int? PropertyStatusId { get; set; }
        public int? FurnitureStatusId { get; set; }
        public int? AreaId { get; set; }
        public decimal? PropertyMinPrice { get; set; }
        public decimal? PropertyMaxPrice { get; set; }
        public string? AvailableFromDate { get; set; }
        public string? AvailableToDate { get; set; }
    }
}