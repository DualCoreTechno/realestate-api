namespace Data.Entity
{
    public class Tbl_PropertyType : BaseEntity
    {
        public string Name { get; set; } = String.Empty;
        public bool IsActive { get; set; } = true;


        public virtual List<Tbl_Segment>? TblSegment { get; set; }
    }
}
