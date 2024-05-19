using System.ComponentModel.DataAnnotations.Schema;

namespace Data.Entity
{
    public class Tbl_Enquiry : BaseEntity
    {
        public string NameOfClient { get; set; } = String.Empty;
        public string MobileNo { get; set; } = String.Empty;
        public string Email { get; set; } = String.Empty;
        public int? EnquiryFor { get; set; }
        public string Remark { get; set; } = String.Empty;
        public string LastRemark { get; set; } = String.Empty;
        public int AssignTo { get; set; }
        public int AssignBy { get; set; }
        public DateTime? Nfd { get; set; }
        public string AreaId { get; set; } = String.Empty;
        public bool? IsClosed { get; set; }
        public string Mobile1 { get; set; } = String.Empty;
        public string Mobile2 { get; set; } = String.Empty;
        public string Mobile3 { get; set; } = String.Empty;


        [ForeignKey("SourceId")]
        public int? SourceId { get; set; }

        [ForeignKey("BhkOfficeId")]
        public int? BhkOfficeId { get; set; }

        [ForeignKey("EnquiryStatusId")]
        public int? EnquiryStatusId { get; set; }

        [ForeignKey("BudgetId")]
        public int? BudgetId { get; set; }

        [ForeignKey("NonuseId")]
        public int? NonuseId { get; set; }

        //[ForeignKey("AreaId")]
        //public int? AreaId { get; set; }


        public virtual Tbl_Source? Source { get; set; }
        public virtual Tbl_BhkOffice? BhkOffice { get; set; }
        public virtual Tbl_EnquiryStatus? EnquiryStatus { get; set; }
        public virtual Tbl_Budget? Budget { get; set; }
        public virtual Tbl_Nonuse? Nonuse { get; set; }
        //public virtual Tbl_Area? Area { get; set; }
    }
}