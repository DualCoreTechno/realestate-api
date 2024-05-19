namespace Data.Entity
{
    public class Tbl_Purpose : BaseEntity
    {
        public string Name { get; set; } = String.Empty;
        public bool IsActive { get; set; } = true;
    }
}
