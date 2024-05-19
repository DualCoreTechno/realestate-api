namespace Data.Entity
{
    public class Tbl_MinMax : BaseEntity
    {
        public string MinTitle { get; set; } = String.Empty;
        public string MaxTitle { get; set; } = String.Empty;
        public int For { get; set; }
        public int MinValue { get; set; }
        public int MaxValue { get; set; }
        public bool IsActive { get; set; } = true;
    }
}
