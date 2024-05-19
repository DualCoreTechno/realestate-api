namespace ViewModels.ViewModels
{
    public class ActivityModel : MasterCommonFieldsModel
    {
        public bool? IsParentActivity { get; set; } = false;
        public int? ParentActivityId { get; set; } = null;
    }
}
