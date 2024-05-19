using System.ComponentModel.DataAnnotations.Schema;

namespace Data.Entity
{
    public class Tbl_ActivityChild : BaseEntity
    {
        public string Name { get; set; } = String.Empty;
        public bool IsActive { get; set; } = true;


        [ForeignKey("ActivityParentId")]
        public int ActivityParentId { get; set; }


        public virtual Tbl_ActivityParent? ActivityParent { get; set; }
    }
}