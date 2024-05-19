using System.ComponentModel.DataAnnotations.Schema;

namespace Data.Entity
{
    public class Tbl_BhkOffice : BaseEntity
    {
        public string Name { get; set; } = String.Empty;
        public bool IsActive { get; set; } = true;

       
        [ForeignKey("SegmentId")]
        public int SegmentId { get; set; }


        public virtual Tbl_Segment? Segment{ get; set; }
        public virtual List<Tbl_Budget>? TblBudget { get; set; }
        public virtual List<Tbl_Property>? Property { get; set; }
        public virtual List<Tbl_PropertyDeal>? PropertyDeal { get; set; }
        public virtual List<Tbl_Enquiry>? Enquiry { get; set; }
    }
}
