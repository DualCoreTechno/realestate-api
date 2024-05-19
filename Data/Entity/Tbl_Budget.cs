using System.ComponentModel.DataAnnotations.Schema;

namespace Data.Entity
{
    public class Tbl_Budget : BaseEntity
    {
        public string Name { get; set; } = String.Empty;
        public int From { get; set; }
        public int To { get; set; }
        public int For { get; set; }
        public bool IsActive { get; set; } = true;


        [ForeignKey("BhkOfficeId")]
        public int BhkOfficeId { get; set; }


        public virtual Tbl_BhkOffice? BhkOffice { get; set; }
        public virtual List<Tbl_Enquiry>? Enquiry { get; set; }
    }
}