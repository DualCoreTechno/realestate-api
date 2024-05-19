namespace Data.Entity
{
    public class Tbl_Area : BaseEntity
    {
        public string Name { get; set; } = String.Empty;
        public bool IsActive { get; set; } = true;

        public virtual List<Tbl_Property>? Property { get; set; }
        //public virtual List<Tbl_Enquiry>? Enquiry { get; set; }
    }
}
