namespace Data.Entity
{
    public class Tbl_ActivityParent : BaseEntity
    {
        public string Name { get; set; } = String.Empty;
        public bool IsActive { get; set; } = true;


        public virtual List<Tbl_ActivityChild>? ActivityChild { get; set; }
    }
}