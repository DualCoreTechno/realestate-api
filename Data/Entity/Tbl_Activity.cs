namespace Data.Entity
{
    public class Tbl_Activity : BaseEntity
    {
        public string Name { get; set; } = String.Empty;
        public bool? IsParent { get; set; } = null;
        public bool IsActive { get; set; } = true;
        public int? ParentActivityId { get; set; } = null;
    }
}
