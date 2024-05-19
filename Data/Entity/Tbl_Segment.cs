using System.ComponentModel.DataAnnotations.Schema;

namespace Data.Entity
{
    public class Tbl_Segment : BaseEntity
    {
        public string Name { get; set; } = String.Empty;
        public bool IsActive { get; set; } = true;


        [ForeignKey("PropertyTypeId")]
        public int PropertyTypeId { get; set; }


        public virtual Tbl_PropertyType? PropertyType { get; set; }
        public virtual List<Tbl_BhkOffice>? TblBhkOffice { get; set; }
    }
}
