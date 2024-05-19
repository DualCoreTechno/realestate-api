namespace Data.Entity
{
    public class Tbl_FurnitureStatus : BaseEntity
    {
        public string Name { get; set; } = String.Empty;
        public bool IsActive { get; set; } = true;
    }
}
