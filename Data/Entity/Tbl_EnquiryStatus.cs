namespace Data.Entity
{
    public class Tbl_EnquiryStatus : BaseEntity
    {
        public string Name { get; set; } = String.Empty;
        public bool IsActive { get; set; } = true;

        public virtual List<Tbl_Enquiry>? Enquiry { get; set; }
    }
}
