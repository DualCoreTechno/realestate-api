namespace Data.Entity
{
    public class Tbl_EnquiryRemarks : BaseEntity
    {
        public int EnquiryId { get; set; }
        public int? ActivityChildId { get; set; }
        public DateTime? Nfd { get; set; }
        public int EnquiryStatusId { get; set; }
        public string Remark { get; set; } = String.Empty;
        public int CreatedBy { get; set; }
    }
}