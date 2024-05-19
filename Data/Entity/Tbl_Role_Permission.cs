using System.ComponentModel.DataAnnotations.Schema;

namespace Data.Entity
{
    public class Tbl_Role_Permission : BaseEntity
    {
        public int MenuId { get; set; }


        [ForeignKey("RoleId")]
        public int RoleId { get; set; }


        public virtual Tbl_Role? Role { get; set; }
    }
}
