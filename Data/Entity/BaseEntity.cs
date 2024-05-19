namespace Data.Entity
{
    public class  BaseEntity
    {
        public int Id { get; set; }
        public int CreatedBy { get; set; } = 0;
        public DateTime CreatedOn { get; set; }
        public int? UpdatedBy { get; set; }
        public DateTime? UpdatedOn { get; set; }
    }
}
