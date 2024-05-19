namespace Data.Entity
{
    public class Tbl_Price : BaseEntity
    {
        public string Name { get; set; } = String.Empty;
        public bool IsActive { get; set; } = true;
    }
}
